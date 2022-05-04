using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Tables;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.Tables
{
    public class EditTableCommand : IRequest<int>
    {
        public MutateTableDto Model { get; }
        public int Id { get; }
        public int FloorplanId { get; }

        public EditTableCommand(MutateTableDto model, int id, int floorplanId)
        {
            Model = model;
            Id = id;
            FloorplanId = floorplanId;
        }
    }

    public class EditDishCommandHandler : IRequestHandler<EditTableCommand, int>
    {
        private readonly IUnitOfWork repository;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public EditDishCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(EditTableCommand request, CancellationToken cancellationToken)
        {
            ValidateModelIds(request);
            logger.Info("trying to edit {@object} with Id: {Id}", request.Model, request.Model.Id);

            var table = await repository.Tables.GetTableById(request.Model.Id, request.Model.FloorplanId);

            if (table is null)
            {
                logger.Error(EntityNotFoundException.Instance);

                throw new EntityNotFoundException();
            }

            table.Seats = request.Model.Seats ?? table.Seats;
            table.Name = request.Model.Name ?? table.Name;
            table.Src = request.Model.Src ?? table.Src;
            table.Type = request.Model.Type ?? table.Type;
            table.Version = request.Model.Version ?? table.Version;
            table.OriginX = request.Model.OriginX ?? table.OriginX;
            table.OriginY = request.Model.OriginY ?? table.OriginY;
            table.Fill = request.Model.Fill ?? table.Fill;
            table.StrokeLineCap = request.Model.StrokeLineCap ?? table.StrokeLineCap;
            table.StrokeLineJoin = request.Model.StrokeLineJoin ?? table.StrokeLineJoin;
            table.BackgroundColor = request.Model.BackgroundColor ?? table.BackgroundColor;
            table.FillRule = request.Model.FillRule ?? table.FillRule;
            table.PaintFirst = request.Model.PaintFirst ?? table.PaintFirst;
            table.GlobalCompositeOperation = request.Model.GlobalCompositeOperation ?? table.GlobalCompositeOperation;
            if (request.Model.Pax != table.Pax)
                table.Pax = request.Model.Pax;
            if (request.Model.Left != table.Left)
                table.Left = request.Model.Left;
            if (request.Model.Top != table.Top)
                table.Top = request.Model.Top;
            if (request.Model.Width != table.Width)
                table.Width = request.Model.Width;
            if (request.Model.Height != table.Height)
                table.Height = request.Model.Height;
            if (request.Model.StrokeWidth != table.StrokeWidth)
                table.StrokeWidth = request.Model.StrokeWidth;
            if (request.Model.StrokeDashOffset != table.StrokeDashOffset)
                table.StrokeDashOffset = request.Model.StrokeDashOffset;
            if (request.Model.StrokeMiterLimit != table.StrokeMiterLimit)
                table.StrokeMiterLimit = request.Model.StrokeMiterLimit;
            if (request.Model.StrokeUniform != table.StrokeUniform)
                table.StrokeUniform = request.Model.StrokeUniform;
            if (request.Model.ScaleX != table.ScaleX)
                table.ScaleX = request.Model.ScaleX;
            if (request.Model.ScaleY != table.ScaleY)
                table.ScaleY = request.Model.ScaleY;
            if (request.Model.Angle != table.Angle)
                table.Angle = request.Model.Angle;
            if (request.Model.FlipX != table.FlipX)
                table.FlipX = request.Model.FlipX;
            if (request.Model.FlipY != table.FlipY)
                table.FlipY = request.Model.FlipY;
            if (request.Model.Opacity != table.Opacity)
                table.Opacity = request.Model.Opacity;
            if (request.Model.Visible != table.Visible)
                table.Visible = request.Model.Visible;
            if (request.Model.SkewX != table.SkewX)
                table.SkewX = request.Model.SkewX;
            if (request.Model.SkewY != table.SkewY)
                table.SkewY = request.Model.SkewY;
            if (request.Model.CropX != table.CropX)
                table.CropX = request.Model.CropX;
            if (request.Model.CropY != table.CropY)
                table.CropY = request.Model.CropY;

            repository.Tables.Update(table);

            await repository.CommitAsync();
            logger.Info("updated {@object} with Id: {id}", table, table.Id);

            return table.Id;
        }

        private static void ValidateModelIds(EditTableCommand request)
        {
            if (request.Model.Id == 0)
            {
                request.Model.Id = request.Id;
            }
            if (request.Model.FloorplanId == 0)
            {
                request.Model.FloorplanId = request.FloorplanId;
            }
        }
    }
}
