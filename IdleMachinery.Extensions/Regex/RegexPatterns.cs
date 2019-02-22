namespace System.Text.RegularExpressions
{
    public static class RegexPatterns
    {        
        //https://www.rhyous.com/2010/06/15/regular-expressions-in-cincluding-a-new-comprehensive-email-pattern/

        /// <summary>
        /// A regex string pattern for identifying an email address
        /// </summary>
        public const string Email = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)" + 
            @"*@((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";


        // https://stackoverflow.com/questions/123559/a-comprehensive-regex-for-phone-number-validation
        // https://regexr.com/38pvb

        /// <summary>
        /// A regex string pattern for identifying a phone number
        /// </summary>
        public const string Phone = @"^\s*(?:\+?(\d{1,3}))?([-. (]*(\d{3})[-. )]*)?((\d{3})" +
            @"[-. ]*(\d{2,4})(?:[-.*e*x*t*\.* ]*(\d+))?)$";
    }
}
