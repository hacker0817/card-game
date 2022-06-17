using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace CardGame.Filters
{
    /// <summary>
    /// 自定义过滤器
    /// </summary>
    public class SwaggerApiExplorer : IActionModelConvention
    {
        /// <summary>
        /// 未标注特性的action将会被忽略
        /// </summary>
        /// <param name="action"></param>
        public void Apply(ActionModel action)
        {
            if (action.Attributes.Count == 0)
                action.ApiExplorer.IsVisible = false;
        }
    }
}
