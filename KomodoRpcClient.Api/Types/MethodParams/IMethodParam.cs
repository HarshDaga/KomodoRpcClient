using Newtonsoft.Json.Schema;

namespace KomodoRpcClient.Api.Types.MethodParams
{
	public interface IMethodParam
	{
		string    Name        { get; }
		string    Description { get; }
		ParamType Type        { get; }

		JSchema GetJsonSchema ( );
	}
}