from src.infrastructure.environment_provider import EnvironmentProvider
from src.infrastructure.rabbitmq import consts
from src.infrastructure.rabbitmq.rabbitmq_helper import RabbitmqHelper


class QueuesHelper:
    def __init__(self):
        self.rabbitmq_helper = RabbitmqHelper()
        self.rabbitmq_configuration = EnvironmentProvider().get_dagair_rabbitmq()
        self.vhost = self.rabbitmq_configuration.vhost

    def create_test_queues(self):
        self.create_sensors_test_queue()
        self.create_measurement_sent_event_test_queue()
        self.create_policies_evaluation_result_event_test_queue()

    def create_sensors_test_queue(self):
        queue_name = consts.sensors_test_queue
        exchange_name = consts.sensors_exchange
        self.rabbitmq_helper.add_new_queue(self.vhost, queue_name)
        self.rabbitmq_helper.bind_queue_to_exchange(self.vhost, exchange_name, queue_name, consts.routing_key)
        self.rabbitmq_helper.purge_queue(self.vhost, queue_name)

    def create_measurement_sent_event_test_queue(self):
        queue_name = consts.measurement_sent_event_test_queue
        exchange_name = consts.measurement_sent_event_exchange
        self.rabbitmq_helper.add_new_queue(self.vhost, queue_name)
        self.rabbitmq_helper.bind_queue_to_exchange(self.vhost, exchange_name, queue_name)
        self.rabbitmq_helper.purge_queue(self.vhost, queue_name)

    def create_policies_evaluation_result_event_test_queue(self):
        queue_name = consts.policies_evaluation_result_event_test_queue
        exchange_name = consts.policies_evaluation_result_event_exchange
        self.rabbitmq_helper.add_new_queue(self.vhost, queue_name)
        self.rabbitmq_helper.bind_queue_to_exchange(self.vhost, exchange_name, queue_name)
        self.rabbitmq_helper.purge_queue(self.vhost, queue_name)


def test_create_test_queues():
    QueuesHelper().create_test_queues()
