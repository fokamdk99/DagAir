from src.infrastructure.environment_provider import EnvironmentProvider
from src.infrastructure.rabbitmq import consts
from src.api_tests.new_measurements import measurement_sent_event_factory
from src.infrastructure.helpers import request_helper
import json
from influxdb_client import InfluxDBClient


def test_when_new_measurement_sent_should_trigger_policies_evaluation_result_event():
    #check that influxdb is available and bucket named 'dagair_bucket' exists
    env_provider = EnvironmentProvider()
    influxdb_path = env_provider.get_influxdb()
    token = "test_token1"
    client = InfluxDBClient(url=influxdb_path, token=token)
    influx_res = client.buckets_api().find_bucket_by_name("dagair_bucket")
    assert influx_res is not None, "Influxdb is not available at the moment or bucket named 'dagair_bucket' does not exist"

    #check that sensors_api is available
    sensors_path = f"{env_provider.get_dagair_sensors()}/system/health/ready"
    sensors_res = request_helper.get_request(sensors_path)
    assert sensors_res.status_code == 204, "Sensors api is not ready."

    rabbitmq_configuration = env_provider.get_dagair_rabbitmq()
    path = f"{rabbitmq_configuration.api_host}/api/exchanges/{rabbitmq_configuration.vhost}/{consts.measurements_from_sensors_exchange}/publish"
    auth = (rabbitmq_configuration.user_name, rabbitmq_configuration.password)
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
        "vhost": rabbitmq_configuration.vhost
    }

    data = json.dumps(data)
    #send new measurement to ingestion node
    res = request_helper.post_request(path, auth, headers, data)
    assert res.status_code == 200, f"call to rabbitmq failed. Status code: {res.status_code}"

