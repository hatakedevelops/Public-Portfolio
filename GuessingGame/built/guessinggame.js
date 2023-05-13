//guessing game code
//class for the computer
var computer = /** @class */ (function () {
    function computer() {
        this.username = "Computer";
    }
    return computer;
}());
//class for the user
var user = /** @class */ (function () {
    function user() {
        this.username = "";
    }
    return user;
}());
// =class includes the player setup and methods for gameplay
var gamePlay = /** @class */ (function () {
    function gamePlay() {
        this.player1 = new user;
        this.player2 = new computer;
    }
    //creates a new player1 with the user's name
    gamePlay.prototype.newPlayer = function () {
        var player1 = new user;
        var name = player1.username;
        prompt("please enter username");
        console.log(prompt);
        name = JSON.stringify(prompt);
    };
    // creates a new player2 with computer as user
    gamePlay.prototype.newComp = function () {
        var player2 = new computer;
        console.log(player2);
    };
    return gamePlay;
}());
//class for making a game choice with methods that choose one of 3 objects
var gameChoice = /** @class */ (function () {
    function gameChoice() {
        this.p1choice = 0;
        this.p2choice = 0;
    }
    gameChoice.prototype.selectRock = function (rock) {
        if (rock === void 0) { rock = 1; }
        if (onclick) {
            this.p1choice = rock;
            console.log(this.p1choice);
        }
    };
    gameChoice.prototype.selectPaper = function (paper) {
        if (paper === void 0) { paper = 2; }
        if (onclick) {
            this.p1choice = paper;
            console.log(this.p1choice);
        }
    };
    gameChoice.prototype.selectScissors = function (scissors) {
        if (scissors === void 0) { scissors = 3; }
        if (onclick) {
            this.p1choice = scissors;
            console.log(this.p1choice);
        }
    };
    gameChoice.prototype.computerSelects = function (pieces) {
        if (pieces === void 0) { pieces = [1, 2, 3]; }
        var rand = Math.floor(Math.random() * pieces.length);
        this.p2choice = pieces[rand];
        console.log(this.p2choice);
    };
    return gameChoice;
}());
function winnerIs() {
    var p1Wins = new gameChoice;
    var p2WIns = new gameChoice;
    var p1 = new gamePlay;
    var p2 = new gamePlay;
    if (p1Wins.p1choice < p2WIns.p2choice) {
        alert("$(p1.player1.username) Loses!");
    }
    else if (p1Wins.p1choice > p2WIns.p2choice) {
        alert("$(p1.player1.username) Wins!");
    }
    else if (p1Wins.p1choice = p1Wins.p2choice) {
        alert("$(p1.player1.username) and Computer are tied!");
    }
}
