
namespace Digiwallet.Wrapper.Models
{
    public class DigiwalletSettings
    {
        public DigiwalletSettings()
        {

        }
        /// <summary>
        /// Currently not used. Use the outlet ID instead. 
        /// </summary>
        public string ApiKey { get; set; }
        /// <summary>
        /// Salt for request signing. Unused. 
        /// </summary>
        public string Salt { get; set; }
    }
}
