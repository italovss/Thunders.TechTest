using Moq;
using Thunders.TechTest.ApiService.Services;
using Thunders.TechTest.Domain.Entities;
using Thunders.TechTest.Domain.Interfaces;

namespace Thunders.TechTest.Tests
{
    public class RelatorioServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IPedagioRepository> _pedagioRepositoryMock;
        private readonly Mock<IRelatorioFaturamentoRepository> _relatorioFaturamentoRepositoryMock;
        private readonly Mock<IRelatorioTopPracasRepository> _relatorioTopPracasRepositoryMock;
        private readonly Mock<IRelatorioVeiculosPorPracaRepository> _relatorioVeiculosPorPracaRepositoryMock;
        private readonly RelatorioService _relatorioService;

        public RelatorioServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _pedagioRepositoryMock = new Mock<IPedagioRepository>();
            _relatorioFaturamentoRepositoryMock = new Mock<IRelatorioFaturamentoRepository>();
            _relatorioTopPracasRepositoryMock = new Mock<IRelatorioTopPracasRepository>();
            _relatorioVeiculosPorPracaRepositoryMock = new Mock<IRelatorioVeiculosPorPracaRepository>();

            _unitOfWorkMock.Setup(u => u.PedagioRepository).Returns(_pedagioRepositoryMock.Object);
            _unitOfWorkMock.Setup(u => u.RelatorioFaturamentoRepository).Returns(_relatorioFaturamentoRepositoryMock.Object);
            _unitOfWorkMock.Setup(u => u.RelatorioTopPracasRepository).Returns(_relatorioTopPracasRepositoryMock.Object);
            _unitOfWorkMock.Setup(u => u.RelatorioVeiculosPorPracaRepository).Returns(_relatorioVeiculosPorPracaRepositoryMock.Object);

            _relatorioService = new RelatorioService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task GerarRelatorioFaturamentoPorHora_Deve_Gerar_Relatorio_Correto()
        {
            // Arrange
            string cidade = "Brasília";
            var pedagioData = new List<PedagioUtilizacao>
            {
                new() { Cidade = cidade, DataHora = new DateTime(2025, 3, 1, 10, 0, 0), ValorPago = 50.0m },
                new() { Cidade = cidade, DataHora = new DateTime(2025, 3, 1, 10, 30, 0), ValorPago = 75.0m },
                new() { Cidade = cidade, DataHora = new DateTime(2025, 3, 1, 11, 0, 0), ValorPago = 100.0m }
            };

            var mockDbSet = DbSetMockHelper.CreateDbSetMock(pedagioData);
            _pedagioRepositoryMock.Setup(repo => repo.Query()).Returns(mockDbSet.Object);

            _relatorioFaturamentoRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<RelatorioFaturamento>()))
                .Returns(Task.CompletedTask);

            // Act
            await _relatorioService.GerarRelatorioFaturamentoPorHora(cidade);

            // Assert
            _relatorioFaturamentoRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<RelatorioFaturamento>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task GerarRelatorioFaturamentoPorHora_Deve_Nao_Gerar_Relatorio_Quando_Nao_Houver_Dados()
        {
            // Arrange
            string cidade = "Cidade Inexistente";
            var pedagioData = new List<PedagioUtilizacao>();

            var mockDbSet = DbSetMockHelper.CreateDbSetMock(pedagioData);
            _pedagioRepositoryMock.Setup(repo => repo.Query()).Returns(mockDbSet.Object);

            // Act
            await _relatorioService.GerarRelatorioFaturamentoPorHora(cidade);

            // Assert
            _relatorioFaturamentoRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<RelatorioFaturamento>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task GerarRelatorioTopPracas_Deve_Gerar_Relatorio_Correto()
        {
            // Arrange
            int quantidade = 2;
            DateTime mesAno = new DateTime(2025, 3, 1);
            var pedagioData = new List<PedagioUtilizacao>
            {
                new() { Praca = "Praça A", DataHora = mesAno, ValorPago = 300.0m },
                new() { Praca = "Praça B", DataHora = mesAno, ValorPago = 500.0m },
                new() { Praca = "Praça C", DataHora = mesAno, ValorPago = 200.0m }
            };

            var mockDbSet = DbSetMockHelper.CreateDbSetMock(pedagioData);
            _pedagioRepositoryMock.Setup(repo => repo.Query()).Returns(mockDbSet.Object);

            _relatorioTopPracasRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<RelatorioTopPracas>()))
                .Returns(Task.CompletedTask);

            // Act
            await _relatorioService.GerarRelatorioTopPracas(mesAno, quantidade);

            // Assert
            _relatorioTopPracasRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<RelatorioTopPracas>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task GerarRelatorioTopPracas_Deve_Nao_Gerar_Relatorio_Quando_Nao_Houver_Dados()
        {
            // Arrange
            int quantidade = 5;
            DateTime mesAno = new(2025, 3, 1);
            var pedagioData = new List<PedagioUtilizacao>();

            var mockDbSet = DbSetMockHelper.CreateDbSetMock(pedagioData);
            _pedagioRepositoryMock.Setup(repo => repo.Query()).Returns(mockDbSet.Object);

            // Act
            await _relatorioService.GerarRelatorioTopPracas(mesAno, quantidade);

            // Assert
            _relatorioTopPracasRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<RelatorioTopPracas>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Never);
        }


        [Fact]
        public async Task GerarRelatorioVeiculosPorPraca_Deve_Gerar_Relatorio_Correto()
        {
            // Arrange
            string praca = "Praça ABC";
            var pedagioData = new List<PedagioUtilizacao>
            {
                new() { Praca = praca, DataHora = DateTime.UtcNow },
                new() { Praca = praca, DataHora = DateTime.UtcNow }
            };

            var mockDbSet = DbSetMockHelper.CreateDbSetMock(pedagioData);
            _pedagioRepositoryMock.Setup(repo => repo.Query()).Returns(mockDbSet.Object);

            _relatorioVeiculosPorPracaRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<RelatorioVeiculosPorPraca>()))
                .Returns(Task.CompletedTask);

            // Act
            await _relatorioService.GerarRelatorioVeiculosPorPraca(praca);

            // Assert
            _relatorioVeiculosPorPracaRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<RelatorioVeiculosPorPraca>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task GerarRelatorioVeiculosPorPraca_Deve_Nao_Gerar_Relatorio_Quando_Nao_Houver_Dados()
        {
            // Arrange
            string praca = "Praça Inexistente";
            var pedagioData = new List<PedagioUtilizacao>();

            var mockDbSet = DbSetMockHelper.CreateDbSetMock(pedagioData);
            _pedagioRepositoryMock.Setup(repo => repo.Query()).Returns(mockDbSet.Object);

            // Act
            await _relatorioService.GerarRelatorioVeiculosPorPraca(praca);

            // Assert
            _relatorioVeiculosPorPracaRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<RelatorioVeiculosPorPraca>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Never);
        }

    }
}
