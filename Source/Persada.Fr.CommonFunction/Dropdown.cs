using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using Persada.Fr.CommonFunction;
using Persada.Fr.ModelView;
using Newtonsoft.Json;

namespace Persada.Fr.Facade
{
    public class Dropdown
    {        
        public static SelectList GetCountryList()
        {
            List<TOURIS_TV_COUNTRY> countries = JsonConvert.DeserializeObject<List<TOURIS_TV_COUNTRY>>(ParsingObject.RequestData(null, "Country", "GridBind", EnumList.IHttpMethod.Get.ToString()));
            TOURIS_TV_COUNTRY country = new TOURIS_TV_COUNTRY { ID = 0, COUNTRY_NAME = "Select Country :" };

            countries.Add(country);
            var countriesList = countries.OrderBy(x => x.ID);

            SelectList selectList = new SelectList(countriesList, "ID", "COUNTRY_NAME");
            return selectList;
        }

        public static SelectList GetProvinceList()
        {
            List<TOURIS_TV_PROVINCE> provinces = JsonConvert.DeserializeObject<List<TOURIS_TV_PROVINCE>>(ParsingObject.RequestData(null, "Province", "GridBind", EnumList.IHttpMethod.Get.ToString()));
            TOURIS_TV_PROVINCE province = new TOURIS_TV_PROVINCE { ID = 0, PROVINCE_NAME = "Select Province :" };

            provinces.Add(province);
            var provinceList = provinces.OrderBy(x => x.ID);

            SelectList selectList = new SelectList(provinceList, "ID", "PROVINCE_NAME");
            return selectList;
        }

        public static SelectList GetProvinceListByCountryId(int countryId)
        {
            List<TOURIS_TV_PROVINCE> provinces = JsonConvert.DeserializeObject<List<TOURIS_TV_PROVINCE>>(ParsingObject.RequestData(countryId, "Province", "GetProvinceByCountryId", EnumList.IHttpMethod.Put.ToString()));
            TOURIS_TV_PROVINCE province = new TOURIS_TV_PROVINCE { ID = 0, PROVINCE_NAME = "Select Province :" };

            provinces.Add(province);
            var provinceList = provinces.OrderBy(x => x.ID);

            SelectList selectList = new SelectList(provinceList, "ID", "PROVINCE_NAME");
            return selectList;
        }

        public static SelectList GetCityList()
        {
            List<TOURIS_TV_CITY> cities = JsonConvert.DeserializeObject<List<TOURIS_TV_CITY>>(ParsingObject.RequestData(null, "City", "GridBind", EnumList.IHttpMethod.Get.ToString()));
            TOURIS_TV_CITY city = new TOURIS_TV_CITY { ID = 0, CITY_NAME = "Select City :" };

            cities.Add(city);
            var cityList = cities.OrderBy(x => x.ID);

            SelectList selectList = new SelectList(cityList, "ID", "CITY_NAME");
            return selectList;
        }

        public static SelectList GetCityListByProvinceId(int provinceId)
        {
            List<TOURIS_TV_CITY> cities = JsonConvert.DeserializeObject<List<TOURIS_TV_CITY>>(ParsingObject.RequestData(provinceId, "City", "GetCityByProvinceId", EnumList.IHttpMethod.Put.ToString()));
            TOURIS_TV_CITY city = new TOURIS_TV_CITY { ID = 0, CITY_NAME = "Select City :" };

            cities.Add(city);
            var cityList = cities.OrderBy(x => x.ID);

            SelectList selectList = new SelectList(cityList, "ID", "CITY_NAME");
            return selectList;
        }

        public static SelectList GetDistrictList()
        {
            List<TOURIS_TV_DISTRICT> districts = JsonConvert.DeserializeObject<List<TOURIS_TV_DISTRICT>>(ParsingObject.RequestData(null, "District", "GridBind", EnumList.IHttpMethod.Get.ToString()));
            TOURIS_TV_DISTRICT district = new TOURIS_TV_DISTRICT { ID = 0, DISTRICT_NAME = "Select District :" };

            districts.Add(district);
            var districtList = districts.OrderBy(x => x.ID);

            SelectList selectList = new SelectList(districtList, "ID", "DISTRICT_NAME");
            return selectList;
        }

        public static SelectList GetDistrictByCityId(int cityId)
        {
            List<TOURIS_TV_DISTRICT> districts = JsonConvert.DeserializeObject<List<TOURIS_TV_DISTRICT>>(ParsingObject.RequestData(cityId, "District", "GetDistrictByCityId", EnumList.IHttpMethod.Put.ToString()));
            TOURIS_TV_DISTRICT district = new TOURIS_TV_DISTRICT { ID = 0, DISTRICT_NAME = "Select District :" };

            districts.Add(district);
            var districtList = districts.OrderBy(x => x.ID);

            SelectList selectList = new SelectList(districtList, "ID", "DISTRICT_NAME");
            return selectList;
        }

        public static SelectList GetVillageList()
        {
            List<TOURIS_TV_VILLAGE> villages = JsonConvert.DeserializeObject<List<TOURIS_TV_VILLAGE>>(ParsingObject.RequestData(null, "Village", "GridBind", EnumList.IHttpMethod.Get.ToString()));
            TOURIS_TV_VILLAGE village = new TOURIS_TV_VILLAGE { ID = 0, VILLAGE_NAME = "Select Village :" };

            villages.Add(village);
            var villageList = villages.OrderBy(x => x.ID);

            SelectList selectList = new SelectList(villageList, "ID", "VILLAGE_NAME");
            return selectList;
        }

        public static SelectList GetVillageByDistrictId(int districtId)
        {
            List<TOURIS_TV_VILLAGE> villages = JsonConvert.DeserializeObject<List<TOURIS_TV_VILLAGE>>(ParsingObject.RequestData(districtId, "Village", "GetVillageByDistrictId", EnumList.IHttpMethod.Put.ToString()));
            TOURIS_TV_VILLAGE village = new TOURIS_TV_VILLAGE { ID = 0, DISTRICT_NAME = "Select District :" };

            villages.Add(village);
            var villageList = villages.OrderBy(x => x.ID);

            SelectList selectList = new SelectList(villageList, "ID", "VILLAGE_NAME");
            return selectList;
        }

        public static SelectList GetNullList()
        {
            List<string> list = new List<string>();
            list.Add("Select :");
            SelectList selectList = new SelectList(list);
            return selectList;
        }

        public static SelectList GetCategoryList()
        {
            List<TOURIS_TV_CATEGORY> categories = JsonConvert.DeserializeObject<List<TOURIS_TV_CATEGORY>>(ParsingObject.RequestData(null, "Category", "GridBind", EnumList.IHttpMethod.Get.ToString()));
            TOURIS_TV_CATEGORY category = new TOURIS_TV_CATEGORY { ID = 0, CATEGORY_NAME = "Select Category :" };

            categories.Add(category);
            var categoryList = categories.OrderBy(x => x.ID);

            SelectList selectList = new SelectList(categoryList, "ID", "CATEGORY_NAME");
            return selectList;
        }
        
        public static SelectList GetGenderList()
        {
            List<CUSTOM> genderList = new List<CUSTOM>();
            string[] genders = new string[] { "L", "P" };
            for (int i = 0; i < genders.Count(); i++)
            {
                CUSTOM gender = new CUSTOM();
                gender.TEXT = genders[i];
                gender.VALUE = genders[i];
                genderList.Add(gender);
            }
            SelectList selectList = new SelectList(genderList, "VALUE", "TEXT");
            return selectList;
        }

        public static SelectList GetTypeSosmedList()
        {
            List<CUSTOM> typeSosmedList = new List<CUSTOM>();
            string[] typeSosmeds = new string[] { "Social", "Embed" };
            for (int i = 0; i < typeSosmeds.Count(); i++)
            {
                CUSTOM typeSosmed = new CUSTOM();
                typeSosmed.TEXT = typeSosmeds[i];
                typeSosmed.VALUE = typeSosmeds[i];
                typeSosmedList.Add(typeSosmed);
            }
            SelectList selectList = new SelectList(typeSosmedList, "VALUE", "TEXT");
            return selectList;
        }

        public static SelectList GetClassActiveList()
        {
            List<CUSTOM> classActiveList = new List<CUSTOM>();
            string[] classActives = new string[] { "active", "inactive" };
            for (int i = 0; i < classActives.Count(); i++)
            {
                CUSTOM classActive = new CUSTOM();
                classActive.TEXT = classActives[i];
                classActive.VALUE = classActives[i];
                classActiveList.Add(classActive);
            }
            SelectList selectList = new SelectList(classActiveList, "VALUE", "TEXT");
            return selectList;
        }

        public static SelectList GetClassButtonList()
        {
            List<CUSTOM> classButtonList = new List<CUSTOM>();
            string[] classButtons = new string[] { "default", "primary" , "success", "warning", "danger"};
            for (int i = 0; i < classButtons.Count(); i++)
            {
                CUSTOM classButton = new CUSTOM();
                classButton.TEXT = classButtons[i];
                classButton.VALUE = classButtons[i];
                classButtonList.Add(classButton);
            }
            SelectList selectList = new SelectList(classButtonList, "VALUE", "TEXT");
            return selectList;
        }
    }

    internal class CUSTOM
    {
        public string TEXT { get; set; }
        public string VALUE { get; set; }
    }
    
}
