using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using VideoGames.Models;

namespace VideoGames.Tests
{

    [TestClass]
    public class GamesTests : IDisposable
    {
        public void Dispose()
        {
            Games.DeleteAll();
        }

        public GamesTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=videogames_tests;";
        }

        [TestMethod]
        public void GetAll_DatabaseEmptyAtFirst_0()
        {
          //Arrange, Act
          int result = Games.GetAll().Count;

          //Assert
          Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Equals_ReturnsTrueIfTitlesAreTheSame_Games()
        {
          //Arrange, Act
          Games firstGame = new Games("Assassin's Creed");
          Games secondGame = new Games("Assassin's Creed");

          //Assert
          Assert.AreEqual(firstGame, secondGame);
        }
        [TestMethod]
        public void Save_SavesToDatabase_Games()
        {
          //Arrange
          Games testGame = new Games("Assassin's Creed");

          //Act
          testGame.Save();
          List<Games> result = Games.GetAll();
          List<Games> testList = new List<Games>{testGame};

          //Assert
          CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void Save_AssignsIdToObject_Id()
        {
          //Arrange
          Games testGame = new Games("Assassin's Creed");

          //Act
          testGame.Save();
          Games savedGame = Games.GetAll()[0];

          int result = savedGame.GetId();
          int testId = testGame.GetId();

          //Assert
          Assert.AreEqual(testId, result);
        }

        [TestMethod]
        public void Find_FindsGamesInDatabase_Games()
        {
          //Arrange
          Games testGame = new Games("Assassin's Creed");
          testGame.Save();

          //Act
          Games foundGame = Games.Find(testGame.GetId());

          //Assert
          Assert.AreEqual(testGame, foundGame);
        }

      }
  }
