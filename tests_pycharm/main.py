# Press Shift+F10 to execute it or replace it with your code.
# Press Double Shift to search everywhere for classes, files, tool windows, actions, and settings.
from src.api_tests.initialize.test_initialize import test_initialize
from src.api_tests.new_measurements.test_new_measurement_should_trigger_policies_evaluation_result_event import \
    test_when_new_measurement_sent_should_trigger_policies_evaluation_result_event

if __name__ == '__main__':
    test_when_new_measurement_sent_should_trigger_policies_evaluation_result_event()

# See PyCharm help at https://www.jetbrains.com/help/pycharm/
