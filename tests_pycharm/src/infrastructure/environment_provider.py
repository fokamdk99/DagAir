import os
import json


class EnvironmentProvider():
    def get_dagair_rabbitmq(self):
        dirname = os.path.dirname(__file__)
        with open(os.path.join(dirname, '../infrastructure/environments.json')) as infrastructure:
            environment = os.environ.get('ENVIRONMENT_NAME')
            data = json.load(infrastructure)

            configuration = data[environment]
            rabbitmq_configuration = RabbitMqConfiguration(configuration["RABBITMQ_API_HOST"],
                                                            configuration["RABBITMQ_VHOST"],
                                                            configuration["RABBITMQ_USERNAME"],
                                                            configuration["RABBITMQ_PASSWORD"])
            return rabbitmq_configuration


class RabbitMqConfiguration():
    def __init__(self, api_host, vhost, user_name, password):
        self.api_host = api_host
        self.vhost = vhost
        self.user_name = user_name
        self.password = password