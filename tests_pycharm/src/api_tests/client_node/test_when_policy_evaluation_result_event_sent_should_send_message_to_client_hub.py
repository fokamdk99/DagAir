from src.infrastructure.environment_provider import EnvironmentProvider
from src.infrastructure.rabbitmq import consts
from src.infrastructure.helpers import request_helper
from src.api_tests.client_node.policies_evaluation_result_event_factory import PoliciesEvaluationResultEvent
import json

def test_when_policy_evaluation_result_event_sent_should_send_message_to_client_hub():
    env_provider = EnvironmentProvider()
    rabbitmq_configuration = env_provider.get_dagair_rabbitmq()
    path = f"{rabbitmq_configuration.api_host}/api/exchanges/{rabbitmq_configuration.vhost}/{consts.policies_evaluation_result_event_exchange}/publish"
    auth = (rabbitmq_configuration.user_name, rabbitmq_configuration.password)
    headers = {"Content-Type": "application/vnd.masstransit+json"}

    payload = {
      "messageId": "a4680000-5d0d-0015-b2ef-08d9a75ae52e",
      "conversationId": "a4680000-5d0d-0015-73f6-08d9a758edf4",
      "sourceAddress": "rabbitmq://localhost/dagair/measurement-sent-event",
      "destinationAddress": "rabbitmq://localhost/dagair/DagAir.PolicyNode.Contracts.Contracts:PoliciesEvaluationResultEvent",
      "messageType": [
        "urn:message:DagAir.PolicyNode.Contracts.Contracts:PoliciesEvaluationResultEvent",
        "urn:message:DagAir.PolicyNode.Contracts.Contracts:IPoliciesEvaluationResultEvent"
      ],
      "message": {
        "message": "Current room condition is compliant with expectations."
      },
      "sentTime": "2021-11-14T10:38:28.3573999Z",
      "headers": {},
      "host": {
        "machineName": "LAPTOP-68JE4R2K",
        "processName": "DagAir.PolicyNode",
        "processId": 27380,
        "assembly": "DagAir.PolicyNode",
        "assemblyVersion": "1.0.0.0",
        "frameworkVersion": "5.0.9",
        "massTransitVersion": "7.2.2.0",
        "operatingSystemVersion": "Microsoft Windows NT 10.0.19042.0"
      }
    }

    data = {
        "delivery_mode": "2",
        "headers": headers,
        "name": consts.policies_evaluation_result_event_exchange,
        "payload": json.dumps(payload),
        "payload_encoding": "string",
        "props": {},
        "properties": {
            "delivery_mode": 2,
            "headers": headers
        },
        "routing_key": "",
        "vhost": rabbitmq_configuration.vhost
    }

    data = json.dumps(data)
    # send new measurement to ingestion node
    res = request_helper.post_request(path, auth, headers, data)
    assert res.status_code == 200, f"call to rabbitmq failed. Status code: {res.status_code}"