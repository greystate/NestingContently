using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.PropertyEditors.ValueConverters;
using Umbraco.Cms.Core.Services;
using Umbraco.Extensions;

namespace NestingContently.Umbraco.ValueConverters;

public class NC_BlockListPropertyValueConverter : BlockListPropertyValueConverter
{
    public NC_BlockListPropertyValueConverter(
        IProfilingLogger logger,
        BlockEditorConverter blockConverter,
        IContentTypeService contentTypeService)
        : base(logger, blockConverter, contentTypeService)
    { }

    public override object? ConvertIntermediateToObject(
        IPublishedElement owner,
        IPublishedPropertyType propertyType,
        PropertyCacheLevel referenceCacheLevel,
        object? inter,
        bool preview)
    {
        var model = (BlockListModel?)base.ConvertIntermediateToObject(owner, propertyType, referenceCacheLevel, inter, preview);

        if (model is not null)
        {
            // The property is currently only supported on content
            var list = model.Where(i => i.Content.IsVisible()).ToList();

            return new BlockListModel(list);
        }

        return model;
    }
}
