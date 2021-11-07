import os
import json

class Environment_provider():
    def get_dagair_rabbitmq():
        dirname = os.path.dirname(__file__)
        with open(os.path.join(dirname, '../infrastructure/environments.json')) as infrastructure:
            data = json.load(infrastructure)
            local_configuration = data["local"]
            rabbitmq_configuration = RabbitMq_configuration(local_configuration["RABBITMQ_API_HOST"],
            local_configuration["RABBITMQ_VHOST"],
            local_configuration["RABBITMQ_USERNAME"],
            local_configuration["RABBITMQ_PASSWORD"])
            return rabbitmq_configuration

class RabbitMq_configuration():
    def __init__(this, api_host, vhost, user_name, password):
        this.api_host = api_host
        this.vhost = vhost
        this.user_name = user_name
        this.password = password