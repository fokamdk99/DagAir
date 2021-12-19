import json
import sys

from src.infrastructure.environment_provider import EnvironmentProvider
from src.infrastructure.helpers import request_helper


class RabbitmqHelper:
    def __init__(self):
        self.rabbitmq_configuration = EnvironmentProvider().get_dagair_rabbitmq()
        self.auth = (self.rabbitmq_configuration.user_name, self.rabbitmq_configuration.password)

    def add_new_queue(self, vhost, queue_name):
        path = f"{self.rabbitmq_configuration.api_host}/api/queues/{vhost}/{queue_name}"
        headers = {}
        data = json.dumps({
            "auto_delete": "false",
            "durable": "true",
            "arguments": {},
        })

        res = request_helper.put_request(path, self.auth, headers, data)

        if res.status_code == 201:
            print(f"Successfully created new queue with name {queue_name}")
            return

        elif res.status_code == 204:
            print(f"Queue already exists.")
            return
        else:
            sys.exit(f"Error while creating queue {queue_name}. Status code {res.status_code} not handled. Please investigate the issue.")

    def bind_queue_to_exchange(self, vhost, exchange_name, queue_name, routing_key=""):
        path = f"{self.rabbitmq_configuration.api_host}/api/bindings/{vhost}/e/{exchange_name}/q/{queue_name}"
        headers = {}
        data = json.dumps({
            "routing_key": routing_key
        })

        res = request_helper.post_request(path, self.auth, headers, data)
        if res.status_code == 201:
            print(f"Successfully bound queue {queue_name} to an exchange {exchange_name}")
            return
        else:
            sys.exit(
                f"Error while binding queue {queue_name} to an exchange {exchange_name}. Status code {res.status_code} not handled. Please investigate the issue.")

    def purge_queue(self, vhost, queue_name):
        path = f"{self.rabbitmq_configuration.api_host}/api/queues/{vhost}/{queue_name}/contents"

        res = request_helper.delete_request(path, self.auth)
        if res.status_code == 204:
            print(f"Successfully purged queue {queue_name}")
            return
        else:
            print(f"Error while purging queue {queue_name}. Status code: {res.status_code}. Please investigate the issue.")

    def get_messages_from_queue(self, vhost, queue_name, number_of_messages, ackmode):
        path = f"{self.rabbitmq_configuration.api_host}/api/queues/{vhost}/{queue_name}/get"

        headers = {}
        data = json.dumps({"count": number_of_messages,
                           "ackmode": ackmode,
                           "encoding": "auto"
                           })

        res = request_helper.post_request(path, self.auth, headers, data)
        return res
