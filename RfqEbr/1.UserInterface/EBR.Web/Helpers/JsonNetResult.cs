using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EBR.Web.Helpers
{
	public class JsonNetResult : ActionResult
	{
		private readonly object _model;

		public JsonNetResult(object model)
		{
			_model = model;
		}

		public override void ExecuteResult(ControllerContext context)
		{
			var response = context.HttpContext.Response;
			response.ContentType = "application/json";

			if (_model != null)
			{
				var writer = new JsonTextWriter(response.Output) { Formatting = Formatting.None };
				var serializer = JsonSerializer.Create(new JsonSerializerSettings
				{
				    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

				serializer.Serialize(writer, _model);
				writer.Flush();
			}
		}

		public static string SerializeObject(object o)
		{
			var settings = new JsonSerializerSettings
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver()
			};
			var a = JsonConvert.SerializeObject(o, settings).Replace("\'", "\\\'").Replace("\\n", "").Replace("\\t", "");
			return a;
		}
	}
}