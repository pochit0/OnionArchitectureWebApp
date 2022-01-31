using Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Product.Commands
{
    using Domain.Entities;

    public class CreateProductCommand : IRequest<int>
    {
        public string Barcode { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public decimal Rate { get; set; }

        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
        {
            private readonly IApplicationDbContext _context;

            public CreateProductCommandHandler(IApplicationDbContext context) => _context = context;

            public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                var product = new Product
                {
                    Barcode = request.Barcode,
                    Name = request.Name,
                    Rate = request.Rate,
                    Description = request.Description
                };

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return product.Id;
            }
        }
    }
}