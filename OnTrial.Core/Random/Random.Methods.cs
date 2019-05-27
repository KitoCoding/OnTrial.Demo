using Dapper;
using OnTrial.Data;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;

namespace OnTrial
{
    public static partial class Random
    {
        private const int MBIG = Int32.MaxValue;
        private const int MSEED = 161803398;
        private const int MZ = 0;

        private static int inext, inextp;
        private static int[] SeedArray = new int[56];

        /// <summary>
        /// 
        /// </summary>
        static Random()
        {
            int ii, mj, mk;
            var seed = Environment.TickCount;

            //Initialize our Seed array.
            //This algorithm comes from Numerical Recipes in C (2nd Ed.)
            int subtraction = (seed == Int32.MinValue) ? Int32.MaxValue : Math.Abs(seed);
            mj = MSEED - subtraction;
            SeedArray[55] = mj;
            mk = 1;
            for (int i = 1; i < 55; i++)
            {  //Apparently the range [1..55] is special (Knuth) and so we're wasting the 0'th position.
                ii = (21 * i) % 55;
                SeedArray[ii] = mk;
                mk = mj - mk;
                if (mk < 0) mk += MBIG;
                mj = SeedArray[ii];
            }
            for (int k = 1; k < 5; k++)
            {
                for (int i = 1; i < 56; i++)
                {
                    SeedArray[i] -= SeedArray[1 + (i + 30) % 55];
                    if (SeedArray[i] < 0) SeedArray[i] += MBIG;
                }
            }
            inext = 0;
            inextp = 21;
            seed = 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static int Sample()
        {
            int retVal;
            int locINext = inext;
            int locINextp = inextp;

            if (++locINext >= 56) locINext = 1;
            if (++locINextp >= 56) locINextp = 1;

            retVal = SeedArray[locINext] - SeedArray[locINextp];

            if (retVal == MBIG) retVal--;
            if (retVal < 0) retVal += MBIG;

            SeedArray[locINext] = retVal;

            inext = locINext;
            inextp = locINextp;

            return retVal;
        }

        /// <summary>
        /// 
        /// </summary>
        public static bool Bool () => Sample() % 2 == 0;

        public static int Int() => Sample();

        public static int Int(int pMaxValue)
        {
            if (pMaxValue <= 0)
                throw new ArgumentOutOfRangeException("maxValue", "maxValue cannot be less than or equal to 0.");
            return (int)(Sample() * (1.0 / MBIG) * pMaxValue);
        }

        public static int Int(int pMinValue, int pMaxValue)
        {
            if (pMinValue > pMaxValue)
                throw new ArgumentOutOfRangeException("minValue", "minValue cannot be greater than maxValue.");

            long? range = (long)pMaxValue - pMinValue;
            if (range <= (long)Int32.MaxValue)
                return ((int)(Sample() * (1.0 / MBIG) * range) + pMinValue);
            else
            {
                var result = Sample();
                var sample = Random.Bool() ? result : -result;
                double d = result;
                d += (Int32.MaxValue - 1);
                d /= 2 * (uint)Int32.MaxValue - 1;
                return (int)((long)(d * range) + pMinValue);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pBuffer"></param>
        /// <param name="pMinValue"></param>
        /// <param name="pMaxValue"></param>
        /// <returns></returns>
        public static byte[] Bytes(int pSize)
        {
            if (pSize == 0)
                throw new ArgumentNullException("pSize", "Buffer Size cannot be 0");

            byte[] buffer = new byte[pSize];

            for (int i = 0; i < buffer.Length; i++)
                buffer[i] = (byte)(Sample() % (Byte.MaxValue + 1));

            return buffer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pSize"></param>
        /// <param name="pMinValue"></param>
        /// <param name="pMaxValue"></param>
        /// <returns></returns>
        public static byte[] Bytes(int pSize, int pMinValue, int pMaxValue)
        {
            if (pSize == 0)
                throw new ArgumentNullException("pSize", "Buffer Size cannot be 0");
            else if (pMinValue == 0 && pMaxValue == 0)
                throw new ArgumentOutOfRangeException("pSize", "Buffer Size min and max parameters cannot equal 0");
            else if (pMinValue > pMaxValue)
                throw new ArgumentOutOfRangeException("pMinValue", "pMinValue cannot be greater than pMaxValue.");

            byte[] buffer = new byte[pSize];

            for (int i = 0; i < buffer.Length; i++)
                buffer[i] = (byte)Random.Int(pMinValue, pMaxValue);

            return buffer;
        }

        public static T Enum<T>(params T[] pExcludes) =>
            System.Enum.GetValues(typeof(T)).Cast<T>().Except(pExcludes).OrderBy(x => Random.Int()).FirstOrDefault();

        public static Color Color() => 
            System.Drawing.Color.FromArgb(Random.Int(255), Random.Int(255), Random.Int(255));

        public static DateTime Date(DateTime pStartDate, DateTime pEndDate) =>
            pStartDate.AddDays(Random.Int((pEndDate - pStartDate).Days));

        public static string FirstName(GenderTypes pGender = GenderTypes.Any)
        {
            switch (pGender)
            {
                case GenderTypes.Any:
                    
                case GenderTypes.Male:
                    return maleFirstNames[Random.Int(maleFirstNames.Length)];
                case GenderTypes.Female:
                    return maleFirstNames[Random.Int(maleFirstNames.Length)];
                default:
                    throw new ArgumentOutOfRangeException("Gender");
            }
        }


        public static string Guid() => 
            System.Guid.NewGuid().ToString();

        public static string LastName() => 
            surnames[Random.Int(surnames.Length)];

        public static string Adjective() => 
            adjectives[Random.Int(adjectives.Length)];

        public static string Noun() => 
            nouns[Random.Int(nouns.Length)];

        public static string Verb() => 
            verbs[Random.Int(verbs.Length)];

        public static char Vowel() => 
            "aeiou".ToCharArray().Random();

        public static char Consonant() => 
            "bcdfghjklmnpqrstvwxyz".ToCharArray().Random();

        public static string Email(string pDomain = null)
        {
            string firstName = Random.FirstName().ToLower();
            string lastName = Random.LastName().ToLower();
            string adj = Random.Adjective().ToLower();
            string noun = Random.Noun().ToLower();
            string domain = (pDomain == null)
                ? string.Format("the{0}{1}", adj, noun).Replace(" ", string.Empty) + Random.TopLevelDomain().ToLower()
                : pDomain;

            return string.Format("{0}{1}@{2}", firstName[0], lastName, domain);
        }

        public static string TopLevelDomain() => topLevelDomains[Random.Int(topLevelDomains.Length)];
        public static string Domain() => domains[Random.Int(domains.Length)];

        /// <summary>
        /// Will query the database for a random country.
        /// </summary>
        /// <returns></returns>
        public static Country Country()
        {
            try
            {
                using (var conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=OnTrial.TCM;Integrated Security=True;"))
                {
                    return conn.Query<Country>($"SELECT Id, Name, Code FROM dbo.Countries WHERE Id = { Random.Int(230)}").FirstOrDefault();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Will query the database for a random region. 
        /// If a <paramref name="pCountryId"/> is supplied, then it will choose a region that is a child of that country. 
        /// If not, then a random region is selected regardless of the parent country.
        /// </summary>
        /// <param name="pCountryId">Defaults to Canada</param>
        /// <returns></returns>
        public static Data.Region Region(int? pCountryId = null)
        {
            try
            {
                using (var conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=OnTrial.TCM;Integrated Security=True;"))
                {
                    var query = pCountryId == null
                        ? $"SELECT * FROM dbo.Regions WHERE Id = {Random.Int(3884)}"
                        : $"SELECT * FROM dbo.Regions WHERE CountryId = {pCountryId}";
                    return conn.Query<Data.Region>(query).FirstOrDefault();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Will query the database for a random region. 
        /// If a <paramref name="pCountryName"/> is supplied, then it will choose a region that is a child of that country. 
        /// If not, then a random region is selected regardless of the parent country.
        /// </summary>
        /// <param name="pCountryName"></param>
        /// <returns></returns>
        public static Data.Region Region(string pCountryName = null)
        {
            try
            {
                using (var conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=OnTrial.TCM;Integrated Security=True;"))
                {
                    if (pCountryName == null)
                        return conn.Query<Data.Region>($"SELECT * FROM dbo.Regions WHERE Id = {Random.Int(3884)}").FirstOrDefault();
                    else
                    {
                        var country = conn.Query<Country>($"Select Top(1) * FROM dbo.Countries WHERE Name Like '%{pCountryName}%'").FirstOrDefault();
                        return conn.Query<Data.Region>($"SELECT * FROM dbo.Regions WHERE CountryId = {country.Id}").FirstOrDefault();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Will query the database for a random City
        /// If a <paramref name="pRegionId"/> is supplied, then it will choose a city that is a child of that region.
        /// If not, then a random city is selected regardless of the parent region.
        /// </summary>
        /// <param name="pRegionId">Defaults to Ontario</param>
        /// <returns></returns>
        public static City City(int? pRegionId = null)
        {
            try
            {
                using (var conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=OnTrial.TCM;Integrated Security=True;"))
                {
                    var query = pRegionId == null
                        ? $"SELECT * FROM dbo.Cities WHERE Id = {Random.Int(5521)}"
                        : $"Select * From dbo.Cities WHERE Id = {conn.Query<int>($"SELECT Id FROM dbo.Cities WHERE RegionId = {pRegionId}").ToArray().Random()}";
                    return conn.Query<City>(query).FirstOrDefault();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Will query the database for a random City
        /// If a <paramref name="pRegionName"/> is supplied, then it will choose a city that is a child of that region.
        /// If not, then a random city is selected regardless of the parent region.
        /// </summary>
        /// <param name="pRegionName"></param>
        /// <returns></returns>
        public static City City(string pRegionName = null)
        {
            try
            {
                using (var conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=OnTrial.TCM;Integrated Security=True;"))
                {
                    if (pRegionName == null)
                        return conn.Query<City>($"SELECT * FROM dbo.Regions WHERE Id = {Random.Int(5521)}").FirstOrDefault();
                    else
                    {
                        var region = conn.Query<Data.Region>($"Select Top(1) * FROM dbo.Regions WHERE Name Like '%{pRegionName}%'").FirstOrDefault();
                        return conn.Query<City>($"SELECT * FROM dbo.Cities WHERE RegionId = {region.Id}").FirstOrDefault();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}