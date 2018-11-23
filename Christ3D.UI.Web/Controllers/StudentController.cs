﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Christ3D.Application.Interfaces;
using Christ3D.Application.ViewModels;
using Christ3D.Domain.Commands;
using Christ3D.Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Christ3D.UI.Web.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentAppService _studentAppService;
        IValidator<StudentViewModel> _validator;
        private IMemoryCache _cache;

        public StudentController(IStudentAppService studentAppService, IMemoryCache cache)
        {
            _studentAppService = studentAppService;
            _cache = cache;
        }

        // GET: Student
        public ActionResult Index()
        {
            return View(_studentAppService.GetAll());
        }

        // GET: Student/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Student/Create
        // 页面
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        // 方法
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentViewModel studentViewModel)
        {
            try
            {
                _cache.Remove("ErrorData");
                //ViewBag.ErrorData = null;
                // 视图模型验证
                if (!ModelState.IsValid)
                    return View(studentViewModel);

                #region 删除命令验证
                ////添加命令验证
                //RegisterStudentCommand registerStudentCommand = new RegisterStudentCommand(studentViewModel.Name, studentViewModel.Email, studentViewModel.BirthDate, studentViewModel.Phone);

                ////如果命令无效，证明有错误
                //if (!registerStudentCommand.IsValid())
                //{
                //    List<string> errorInfo = new List<string>();
                //    //获取到错误，请思考这个Result从哪里来的 
                //    foreach (var error in registerStudentCommand.ValidationResult.Errors)
                //    {
                //        errorInfo.Add(error.ErrorMessage);
                //    }
                //    //对错误进行记录，还需要抛给前台
                //    ViewBag.ErrorData = errorInfo;
                //    return View(studentViewModel);
                //} 
                #endregion

                // 执行添加方法
                _studentAppService.Register(studentViewModel);

                var errorData = _cache.Get("ErrorData");
                if (errorData == null)
                    ViewBag.Sucesso = "Student Registered!";

                return View(studentViewModel);
            }
            catch (Exception e)
            {
                return View(e.Message);
            }
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Student/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}