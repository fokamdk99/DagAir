#nullable enable
using System;
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
            var policy = TestPoliciesDataService.CreateNewRoomPolicyDto(15, 80, (decimal) 0.3, 2, 20, (decimal) 0.1);

            var result = _policyEvaluator!.Evaluate(currentMeasurement, policy);
            
            Assert.AreEqual(result.TemperatureStatus, EvaluatorResult.TooHigh);
            Assert.AreEqual(result.IlluminanceStatus, EvaluatorResult.TooHigh);
            Assert.AreEqual(result.HumidityStatus, EvaluatorResult.TooHigh);
        }
        
        [Test]
        public void WhenMeasurementsAreTooLow_ShouldReturnTooLowIndicators()
        {
            var currentMeasurement = CreateMeasurementSentEvent();
            var policy = TestPoliciesDataService.CreateNewRoomPolicyDto(25, 160, (decimal) 0.7, 2, 20, (decimal) 0.1);

            var result = _policyEvaluator!.Evaluate(currentMeasurement, policy);
            
            Assert.AreEqual(result.TemperatureStatus, EvaluatorResult.TooLow);
            Assert.AreEqual(result.IlluminanceStatus, EvaluatorResult.TooLow);
            Assert.AreEqual(result.HumidityStatus, EvaluatorResult.TooLow);
        }
        
        [Test]
        public void WhenMeasurementsAreInOrder_ShouldReturnInOrderMessage()
        {
            var currentMeasurement = CreateMeasurementSentEvent();
            var policy = TestPoliciesDataService.CreateNewRoomPolicyDto(21, 120, (decimal) 0.5, 2, 20, (decimal) 0.1);

            var result = _policyEvaluator!.Evaluate(currentMeasurement, policy);
            
            Assert.AreEqual(result.TemperatureStatus, EvaluatorResult.Normal);
            Assert.AreEqual(result.IlluminanceStatus, EvaluatorResult.Normal);
            Assert.AreEqual(result.HumidityStatus, EvaluatorResult.Normal);
            Assert.AreEqual(result.Message, MessageConsts.MeasurementsInOrder);
        }
        
        internal static MeasurementSentEvent CreateMeasurementSentEvent()
        {
            return new MeasurementSentEvent(21, 120, (decimal)0.5, "sensorName1");
        }
    }
}