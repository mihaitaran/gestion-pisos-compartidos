using FluentAssertions;
using GestionPisosCompartidos.Models.Entities;
using GestionPisosCompartidos.Repositories.Interfaces;
using GestionPisosCompartidos.Services.Implementations;
using Moq;

namespace GestionPisosCompartidos.Tests
{
    public class InquilinoViviendaServiceTests
    {
        private readonly Mock<IInquilinoViviendaRepository> _repoMock;
        private readonly InquilinoViviendaService _service;

        public InquilinoViviendaServiceTests()
        {
            _repoMock = new Mock<IInquilinoViviendaRepository>();
            _service = new InquilinoViviendaService(_repoMock.Object);
        }

        [Fact]
        public async Task CreateAsync_InquilinoConViviendaActiva_LanzaExcepcion()
        {
            var asignacionActiva = new InquilinosVivienda
            {
                Id = 1,
                InquilinoId = 5,
                ViviendaId = 1,
                Activo = true
            };

            _repoMock.Setup(r => r.GetByInquilinoIdAsync(5))
                .ReturnsAsync(new List<InquilinosVivienda> { asignacionActiva });

            var nuevaAsignacion = new InquilinosVivienda
            {
                InquilinoId = 5,
                ViviendaId = 2,
                Activo = true
            };

            var accion = async () => await _service.CreateAsync(nuevaAsignacion);

            await accion.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("*vivienda activa*");
        }

        [Fact]
        public async Task CreateAsync_InquilinoSinVivienda_CreaAsignacion()
        {
            _repoMock.Setup(r => r.GetByInquilinoIdAsync(5))
                .ReturnsAsync(new List<InquilinosVivienda>());

            var nuevaAsignacion = new InquilinosVivienda
            {
                InquilinoId = 5,
                ViviendaId = 1
            };

            _repoMock.Setup(r => r.CreateAsync(It.IsAny<InquilinosVivienda>()))
                .ReturnsAsync(nuevaAsignacion);

            var resultado = await _service.CreateAsync(nuevaAsignacion);

            resultado.Should().NotBeNull();
            resultado.Activo.Should().BeTrue();
        }
    }
}