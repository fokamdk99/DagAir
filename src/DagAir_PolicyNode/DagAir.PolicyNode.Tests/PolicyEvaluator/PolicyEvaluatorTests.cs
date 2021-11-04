#nullable enable
using DagAir.IngestionNode.Contracts;
using DagAir.PolicyNode.Contracts.Contracts;
using DagAir.PolicyNode.PolicyEvaluator;
using NUnit.Framework;

namespace DagAir.PolicyNode.Tests.PolicyEvaluator
{
    [Category("Unit")]
    public class PolicyEvaluatorTests
    {
        private IPolicyEvaluator? _policyEvaluator;

        [SetUp]
        public void Setup()
        {
            _policyEvaluator = new PolicyNode.PolicyEvaluator.PolicyEvaluator();
        }

        [Test]
        public void WhenMeasurementsAreTooHigh_ShouldReturnTooHighIndicators()
        {
            var currentMeasurement = CreateMeasurementSentEvent();
            var policy = TestPoliciesDataService.CreateNewRoomPolicyDto(15, 80, 0.3f, 2, 20, 0.1f);

            var result = _policyEvaluator!.Evaluate(currentMeasurement, policy);
            
            Assert.AreEqual(result.TemperatureStatus, EvaluatorResult.TooHigh);
            Assert.AreEqual(result.IlluminanceStatus, EvaluatorResult.TooHigh);
            Assert.AreEqual(result.HumidityStatus, EvaluatorResult.TooHigh);
        }
        
        [Test]
        public void WhenMeasurementsAreTooLow_ShouldReturnTooLowIndicators()
        {
            var currentMeasurement = CreateMeasurementSentEvent();
            var policy = TestPoliciesDataService.CreateNewRoomPolicyDto(25, 160, 0.7f, 2, 20, 0.1f);

            var result = _policyEvaluator!.Evaluate(currentMeasurement, policy);
            
            Assert.AreEqual(result.TemperatureStatus, EvaluatorResult.TooLow);
            Assert.AreEqual(result.IlluminanceStatus, EvaluatorResult.TooLow);
            Assert.AreEqual(result.HumidityStatus, EvaluatorResult.TooLow);
        }
        
        [Test]
        public void WhenMeasurementsAreInOrder_ShouldReturnInOrderMessage()
        {
            var currentMeasurement = CreateMeasurementSentEvent();
            var policy = TestPoliciesDataService.CreateNewRoomPolicyDto(21, 120, 0.5f, 2, 20, 0.1f);

            var result = _policyEvaluator!.Evaluate(currentMeasurement, policy);
            
            Assert.AreEqual(result.TemperatureStatus, EvaluatorResult.Normal);
            Assert.AreEqual(result.IlluminanceStatus, EvaluatorResult.Normal);
            Assert.AreEqual(result.HumidityStatus, EvaluatorResult.Normal);
            Assert.AreEqual(result.Message, MessageConsts.MeasurementsInOrder);
        }
        
        internal static MeasurementSentEvent CreateMeasurementSentEvent()
        {
            return new MeasurementSentEvent(21, 120, 0.5F, 1);
        }
    }
}