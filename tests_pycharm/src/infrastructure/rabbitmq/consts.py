measurements_from_sensors_exchange = "amq.topic"
routing_key = "room_measurements"

sensors_exchange = "amq.topic"
sensors_test_queue = "sensors.testqueue"

measurement_sent_event_exchange = "DagAir.IngestionNode.Contracts:MeasurementSentEvent"
measurement_sent_event_test_queue = "MeasurementSentEvent.testqueue"

policies_evaluation_result_event_exchange = "DagAir.PolicyNode.Contracts.Contracts:PoliciesEvaluationResultEvent"
policies_evaluation_result_event_test_queue = "PoliciesEvaluationResultEvent.testqueue"
