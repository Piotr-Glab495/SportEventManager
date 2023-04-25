﻿using System.ComponentModel.DataAnnotations;
using SportEventManager.Core.TeamAggregate;
using SportEventManager.Web.ViewModels.TeamModel.Stats;

namespace SportEventManager.Web.ViewModels.TeamModel;

public class TeamViewModel
{
  public int Id { get; set; }

  public string Name { get; set; } = string.Empty;

  public string City { get; set; } = string.Empty;

  [Range(1, 50)]
  public int NumberOfPlayers { get; set; }

  public string OwnerId { get; private set; } = string.Empty;

  public bool IsDeleted { get; set; } = false;


  public List<PlayerViewModel> Players { get; set; } = new List<PlayerViewModel>();

  public FBTeamStatsViewModel? FbTeamStats { get; set; }

  public static TeamViewModel FromTeam(Team team) => new()
  {
    Id = team.Id,
    Name = team.Name,
    City = team.City,
    NumberOfPlayers = team.NumberOfPlayers,
    OwnerId = team.OwnerId,
    IsDeleted= team.IsDeleted,
    Players = team.Players.Select(p => PlayerViewModel.FromPlayer(p)).ToList(),
    FbTeamStats = FBTeamStatsViewModel.FromTeamStats(team.FbTeamStats)
  };
}