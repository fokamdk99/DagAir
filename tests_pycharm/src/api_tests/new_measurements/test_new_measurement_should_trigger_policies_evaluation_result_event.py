import sys
import time

from src.infrastructure.environment_provider import EnvironmentProvider
from src.infrastructure.rabbitmq import consts
from src.api_tests.new_measurements import measurement_sent_event_factory
from src.infrastructure.helpers import request_helper
import json
from influxdb_client import InfluxDBClient
from src.infrastructure.rabbitmq.rabbitmq_helper import RabbitmqHelper


class NewMeasurementTestHelper:
    def __init__(self):
        self.env_provider = EnvironmentProvider()
        self.rabbitmq_configuration = self.env_provider.get_dagair_rabbitmq()
        self.auth = auth = (self.rabbitmq_configuration.user_name, self.rabbitmq_configuration.password)
        self.rabbitmq_helper = RabbitmqHelper()

    def check_influxdb_is_available(self):
        path = self.env_provider.get_influxdb()
        token = self.env_provider.get_influxdb_token()
        bucket_name = self.env_provider.get_influxdb_bucket()
        client = InfluxDBClient(url=path, token=token)
        influx_res = client.buckets_api().find_bucket_by_name(bucket_name)
        assert influx_res is not None, f"Influxdb is not available at the moment or bucket named {bucket_name} " \
                                       f"does not exist"

    def check_sensors_api_is_available(self):
        path = f"{self.env_provider.get_dagair_sensors()}/system/health/ready"
        sensors_res = request_helper.get_request(path)
        assert sensors_res.status_code == 204, "Sensors api is not ready."

    def send_new_measurement_to_rabbitmq(self):
        path = f"{self.rabbitmq_configuration.api_host}/api/exchanges/{self.rabbitmq_configuration.vhost}/{consts.measurements_from_sensors_exchange}/publish"
        headers = {}
        data = {
            "delivery_mode": "1",
            "headers": headers,
            "name": consts.measurements_from_sensors_exchange,
            "payload": measurement_sent_event_factory.generate_measurement_sent_event_json(),
            "payload_encoding": "string",
            "props": {},
            "properties": {
                "delivery_mode": 1,
                "headers": {}
            },
            "routing_key": consts.routing_key,
            "vhost": self.rabbitmq_configuration.vhost
        }

        data = json.dumps(data)
        # send new measurement to ingestion node
        res = request_helper.post_request(path, self.auth, headers, data)
        assert res.status_code == 200, f"call to rabbitmq failed. Status code: {res.status_code}"

    def check_message_sent_to_queue(self, vhost, queue_name, number_of_messages):
        for i in range(1, 5):
            time.sleep(3)
            res = self.rabbitmq_helper.get_messages_from_queue(vhost, queue_name, number_of_messages,
                                                               "ack_requeue_true")
            assert res.status_code == 200, f"Error while trying to get messages from queue {queue_name}. " \
                                           f"Status_code: {res.status_code}. Please investigate the issue."
            content = json.loads(res.content)
            if len(content) == number_of_messages:
                self.rabbitmq_helper.purge_queue(vhost, queue_name)
                return

        sys.exit(f"Retrieved number of messages: {len(content)} is inconsistent with expected number of messages: "
                 f"{number_of_messages}. Please investigate the issue.")


def test_when_new_measurement_sent_should_trigger_policies_evaluation_result_event():
    new_measurement_test_helper = NewMeasurementTestHelper()

    # check that influxdb is available and bucket exists
    new_measurement_test_helper.check_influxdb_is_available()

    # check that sensors_api is available
    new_measurement_test_helper.check_sensors_api_is_available()

    # send new measurement to rabbitmq
    new_measurement_test_helper.send_new_measurement_to_rabbitmq()

    # check that new measurement has been sent to sensors test queue
    new_measurement_test_helper.check_message_sent_to_queue(new_measurement_test_helper.rabbitmq_configuration.vhost,
                                                            consts.sensors_test_queue, 1)

    # check that MeasurementSentEvent has been sent to test queue
    new_measurement_test_helper.check_message_sent_to_queue(new_measurement_test_helper.rabbitmq_configuration.vhost,
                                                            consts.measurement_sent_event_test_queue, 1)

    # check that PoliciesEvaluationResultEvent has been sent to test queue
    new_measurement_test_helper.check_message_sent_to_queue(new_measurement_test_helper.rabbitmq_configuration.vhost,
                                                            consts.policies_evaluation_result_event_test_queue, 1)

