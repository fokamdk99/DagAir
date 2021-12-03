from src.api_tests.new_measurements.test_new_measurement_should_trigger_policies_evaluation_result_event import \
    NewMeasurementTestHelper


def test_when_new_measurement_sent_should_trigger_policies_evaluation_result_event():
    new_measurement_test_helper = NewMeasurementTestHelper()

    # send new measurement to rabbitmq
    new_measurement_test_helper.send_new_measurement_to_rabbitmq()