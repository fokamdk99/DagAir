import os
import json


class EnvironmentProvider():
    def get_configuration(self):
        dirname = os.path.dirname(__file__)
        with open(os.path.join(dirname, '../infrastructure/environments.json')) as infrastructure:
            environment = os.environ.get('ENVIRONMENT_NAME')
            data = json.load(infrastructure)
            configuration = data[environment]

            return configuration

    def get_influxdb(self):
        configuration = self.get_configuration()
        return configuration["INFLUXDB"]

    def get_influxdb_token(self):
        configuration = self.get_configuration()
        return configuration["INFLUXDB_TOKEN"]

    def get_influxdb_bucket(self):
        configuration = self.get_configuration()
        return configuration["INFLUXDB_BUCKET"]

    def get_dagair_policies(self):
        configuration = self.get_configuration()
        return configuration["DAGAIR_POLICIES"]

    def get_dagair_sensors(self):
        configuration = self.get_configuration()
        return configuration["DAGAIR_SENSORS"]

    def get_dagair_rabbitmq(self):
        configuration = self.get_configuration()
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