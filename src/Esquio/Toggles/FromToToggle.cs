using Esquio.Abstractions;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace Esquio.Toggles
{
    [DesignType(Description = "Toggle that is active depending on current UTC date.")]
    [DesignTypeParameter(ParameterName = From, ParameterType = EsquioConstants.DATE_PARAMETER_TYPE, ParameterDescription = "The from date (yyyy-MM-dd HH:mm:ss) interval when this toggle is activated.")]
    [DesignTypeParameter(ParameterName = To, ParameterType = EsquioConstants.DATE_PARAMETER_TYPE, ParameterDescription = "The to date (yyyy-MM-dd HH:mm:ss) interval when this toggle is activated.")]
    public class FromToToggle
        : IToggle
    {
        internal const string FORMAT_DATE = "yyyy-MM-dd HH:mm:ss";
        internal const string From = nameof(From);
        internal const string To = nameof(To);

        private readonly IRuntimeFeatureStore _featureStore;

        public FromToToggle(IRuntimeFeatureStore featureStore)
        {
            _featureStore = featureStore ?? throw new ArgumentNullException(nameof(featureStore));
        }

        public async Task<bool> IsActiveAsync(string featureName, string productName = null, CancellationToken cancellationToken = default)
        {
            var feature = await _featureStore.FindFeatureAsync(featureName, productName, cancellationToken);
            var toggle = feature.GetToggle(this.GetType().FullName);
            var data = toggle.GetData();

            var fromDate = DateTime.ParseExact(data.From.ToString(), FORMAT_DATE, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
            var toDate = DateTime.ParseExact(data.To.ToString(), FORMAT_DATE, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
            var now = DateTime.UtcNow;

            if (now > fromDate && now < toDate)
            {
                return true;
            }
            return false;
        }
    }
}