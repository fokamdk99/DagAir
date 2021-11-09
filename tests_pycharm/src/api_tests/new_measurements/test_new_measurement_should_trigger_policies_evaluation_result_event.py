from src.infrastructure.environment_provider import EnvironmentProvider
from src.infrastructure.rabbitmq import consts
from src.api_tests.new_measurements import measurement_sent_event_factory
from src.infrastructure.helpers import request_helper
import json


def test_when_new_measurement_sent_should_trigger_policies_evaluation_result_event():
    rabbitmq_configuration = EnvironmentProvider().get_dagair_rabbitmq()
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
    res = request_helper.post_request(path, auth, headers, data)
    assert res.status_code == 200, f"call to rabbitmq failed. Status code: {res.status_code}"