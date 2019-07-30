using DynamicLookupModule.Models;
using DynamicLookupModule.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DynamicLookupModule.Controllers
{
    public class LookupsController : Controller
    {
        LookupService _lookupService;
        IMapper _mapper;

        public LookupsController(LookupService lookupService, IMapper mapper)
        {
            _lookupService = lookupService;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult Lookups()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LoadLookupInfoPartial(int lType)
        {
            LookupModel model = new LookupModel();

            Type type = _lookupService.TypeById(lType);
            if (type != null)
            {
                model.LookupHeader = (BaseLookup)Assembly.GetExecutingAssembly().CreateInstance(_lookupService.NameById(lType));
                model.LookupList = _mapper.Map<List<BaseLookup>>(typeof(LookupService).GetMethod("GetLookupList").MakeGenericMethod(type).Invoke(_lookupService, new object[] { }));
            }
            return PartialView("Partial/LookupInfo", model);
        }

        [HttpGet]
        public ActionResult LoadLookupHeader(int lType, int Id)
        {
            BaseLookup model = null;
            Type type = _lookupService.TypeById(lType);
            if (type != null)
            {
                model = _mapper.Map<BaseLookup>(typeof(LookupService).GetMethod("GetLookup").MakeGenericMethod(type).Invoke(_lookupService, new object[] { Id }));
            }
            return PartialView("Partial/LookupHeader", model);
        }

        [HttpGet]
        public ActionResult LoadLookupList(int lType)
        {
            List<BaseLookup> model = null;
            Type type = _lookupService.TypeById(lType);
            if (type != null)
            {
                model = _mapper.Map<List<BaseLookup>>(typeof(LookupService).GetMethod("GetLookupList").MakeGenericMethod(type).Invoke(_lookupService, new object[] { }));
            }
            return PartialView("Partial/LookupList", model);
        }

        [HttpPost]
        public ActionResult SaveLookup([FromBody] JObject data)
        {
            bool res = false;
            int lType = data["lType"].ToObject<int>();
            BaseLookup lookup = data["lookup"].ToObject<BaseLookup>();

            if (lookup == null || string.IsNullOrEmpty(lookup.Name.Trim()))
                return Json(new { success = false, response = "Name Is Required!" });
             
            string MethodName = string.Empty;
            Type type = _lookupService.TypeById(lType);
            if (type != null)
            {
                var obj = typeof(JsonConvert).GetMethods().Where(x => x.Name == "DeserializeObject").ToList()[3].MakeGenericMethod(type)
                    .Invoke(null, new [] { JsonConvert.SerializeObject(lookup) });

                if (lookup.Id > 0)
                    MethodName = "UpdateLookup";
                else
                    MethodName = "AddLookup";

                res = (bool)typeof(LookupService).GetMethod(MethodName).MakeGenericMethod(type).Invoke(_lookupService, new object[] { obj });
            }

            return Json(new { success = res, response = res ? "Saved Successfully!" : "Error Whrn Delete!" });
        }

        [HttpPost]
        public ActionResult DeleteLookup([FromBody] JObject data)
        {
            int lType = data["lType"].ToObject<int>();
            int Id = data["id"].ToObject<int>();

            bool res = false;
            Type type = _lookupService.TypeById(lType);
            if (type != null)
            {
                res = (bool)typeof(LookupService).GetMethod("RemoveLookup").MakeGenericMethod(type).Invoke(_lookupService, new object[] { Id });
            }

            return Json(new { success = res, response = res ? "Deleted Successfully!" : "Error Whrn Delete!" });
        }

    }
}
