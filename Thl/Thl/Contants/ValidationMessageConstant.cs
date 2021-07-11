using System.ComponentModel;

namespace Thl.Contants
{
    public enum ValidationMessageConstant
    {
        [Description("Cannot find id, please check and retry.")]
        NOT_FOUND_ID,

        [Description("Invalid parameter, please check and retry.")]
        INVALID_PARAM,

        [Description("Invalid request, please check and retry.")]
        INVALID_REQUEST,

        [Description("Cannot find corresponding data to operate the action, please check and retry.")]
        NOT_FOUND_DATA
    }
}