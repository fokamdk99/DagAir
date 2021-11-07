import requests
import json
from urllib3.util.retry import Retry
from requests.adapters import HTTPAdapter
from ...infrastructure.environment_provider import Environment_provider

def initialize():
    rabbitmq_configuration = Environment_provider.get_dagair_rabbitmq()
    path = rabbitmq_configuration.api_host + "/api/queues/" + rabbitmq_configuration.vhost

    s = requests.Session()

    retries = Retry(total=5,
                    backoff_factor=0.2)

    s.mount('http://', HTTPAdapter(max_retries=retries))

    res = s.get(path, auth=(rabbitmq_configuration.user_name, rabbitmq_configuration.password))

    assert res.status_code == 200, "Call to rabbitMQ api failed"

    content = json.loads(res.content)
    error_queues = [queue for queue in content if "_error" in queue["name"]]
    if len(error_queues) > 0:
        number_of_error_messages = [sum(queue["messages"]) for queue in error_queues]
        assert number_of_error_messages == 0, "There are error messages on rabbitMQ prior to any tests, please fix them first."

initialize()