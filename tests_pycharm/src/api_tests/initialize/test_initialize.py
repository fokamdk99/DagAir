import json

from src.infrastructure.environment_provider import EnvironmentProvider
from src.infrastructure.helpers import request_helper


def test_initialize():
    rabbitmq_configuration = EnvironmentProvider().get_dagair_rabbitmq()
    path = rabbitmq_configuration.api_host + "/api/queues/" + rabbitmq_configuration.vhost

    auth = (rabbitmq_configuration.user_name, rabbitmq_configuration.password)
    res = request_helper.get_request(path, auth)

    assert res.status_code == 200, "Call to rabbitMQ api failed"

    content = json.loads(res.content)
    error_queues = [queue for queue in content if "_error" in queue["name"]]
    if len(error_queues) > 0:
        number_of_error_messages = [sum(queue["messages"]) for queue in error_queues]
        assert number_of_error_messages == 0, "There are error messages on rabbitMQ prior to any tests, please fix them first."
