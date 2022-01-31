using Application.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Product.Commands
{
    public class UpdateProductCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }

        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, int>
        {
            private readonly IApplicationDbContext _context;

            public UpdateProductCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                var product = _context.Products.Where(a => a.Id == request.Id).FirstOrDefault();
                if (product == null) return default;

                product.Barcode = request.Barcode;
                product.Name = request.Name;
                product.Rate = request.Rate;
                product.Description = request.Description;

                await _context.SaveChangesAsync();

                return product.Id;
            }
        }
    }
}