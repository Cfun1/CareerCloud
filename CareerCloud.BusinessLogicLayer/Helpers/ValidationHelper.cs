namespace CareerCloud.BusinessLogicLayer.Helpers;
internal static class ValidationHelper
{
    internal static bool IsPhoneValide(string phoneNumber)
    {
        string[] phoneComponents = phoneNumber.Split('-');
        if (phoneComponents.Length != 3)
        {
            return false;
        }
        else
        {
            if (phoneComponents[0].Length != 3)
            {
                return false;

            }
            else if (phoneComponents[1].Length != 3)
            {
                return false;

            }
            else if (phoneComponents[2].Length != 4)
            {
                return false;

            }
        }
        return true;
    }
}
