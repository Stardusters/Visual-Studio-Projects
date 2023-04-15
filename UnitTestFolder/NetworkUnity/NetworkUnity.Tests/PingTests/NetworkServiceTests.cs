using FakeItEasy;
using FluentAssertions;
using NetworkUnity.DNS;
using NetworkUnity.Ping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NetworkUnity.Tests.PingTests
{
    public class NetworkServiceTests
    {
        private readonly NetworkService _pingService;
        private readonly IDNS _dns;
        public NetworkServiceTests()
        {
            _dns = A.Fake<IDNS>();
            _pingService = new NetworkService(_dns);
        }
        [Fact]
        public void NetworkService_SendPing_ReturnString()
        {
            //Arrange
            A.CallTo(() => _dns.sendDNS()).Returns(true);
            //Act
            var result = _pingService.SendPing();
            //Assert
            result.Should().NotBeNull();
            result.Should().NotBeEmpty("because the string is not empty");
            result.Should().NotBeNullOrWhiteSpace();
            result.Should().Be("Success: Ping Sent!");
        }
        [Theory]
        [InlineData(1,1,2)]
        [InlineData(2,2,4)]
        public void NetworkService_PingTimeOut_ReturnInt(int a, int b, int expection)
        {
            //Arrange
            //Act
            var result = _pingService.PingTimeOut(a, b);
            //Assert
            result.Should().Be(expection);
            result.Should().BeGreaterThanOrEqualTo(2);
            result.Should().NotBeInRange(-10000, 0);
        }

        [Fact]
        public void NetworkService_LastPingDate_ReturnDateTime()
        {
            //Arrange
            //Act
            var result = _pingService.LastPingDate();
            //Assert
            result.Should().HaveYear(2023);
            result.Should().HaveMonth(4);
            result.Should().HaveDay(12);
        }

        [Fact]  
        public void NetworkService_GetPingOptions_ReturnObject()
        {
            //Arrange
            var expected = new PingOptions()
            {
                DontFragment = true,
                Ttl = 1
            };
            //Act
            var result = _pingService.GetPingOptions();
            //Assert
            result.Should().BeOfType<PingOptions>();
            result.Should().BeEquivalentTo(expected);
            result.Ttl.Should().Be(1);
        }

        [Fact]
        public void NetworkService_MostRecentPings_ReturnObject()
        {
            //Arrange
            var expected = new PingOptions()
            {
                DontFragment = true,
                Ttl = 1
            };
            //Act
            var result = _pingService.MostRecentPings();
            //Assert
            //result.Should().BeOfType<PingOptions>();
            result.Should().ContainEquivalentOf(expected);
            result.Should().Contain(x => x.DontFragment == true);
        }
    }
}
