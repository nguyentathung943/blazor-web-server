﻿using Business.Repository.IRepository;
using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;


namespace HIddenVila_API.Controllers;

[Route("api/[controller]")]
public class HotelRoomController : Controller
{
    private readonly IHotelRoomRepository _hotelRoomRepository;

    public HotelRoomController(IHotelRoomRepository hotelRoomRepository)
    {
        _hotelRoomRepository = hotelRoomRepository;
    }

    [HttpGet]
    [Authorize(Roles = SD.Role_Admin)]
    public async Task<IActionResult> GetHotelRooms()
    {
        var allRooms = await _hotelRoomRepository.GetAllHotelRooms();
        return Ok(allRooms);
    }

    [HttpGet("{roomId}")]
    public async Task<IActionResult> GetHotelRoom(int? roomId)
    {
        if (roomId == null)
        {
            return BadRequest(new ErrorModel()
            {
                Title = "",
                ErrorMessage = "Invalid Room Id",
                StatusCode = StatusCodes.Status400BadRequest
            });
        }
        
        var hotelRoom = await _hotelRoomRepository.GetHotelRoom(roomId.Value);
        if (hotelRoom == null)
        {
            return BadRequest(new ErrorModel()
            {
                Title = "",
                ErrorMessage = "Invalid Room Id",
                StatusCode = StatusCodes.Status404NotFound
            });
        }
        return Ok(hotelRoom);
    }
}
