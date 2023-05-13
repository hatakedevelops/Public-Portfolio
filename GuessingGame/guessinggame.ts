//guessing game code


//class for the computer
class computer {
    username: string =  "Computer";
}

//class for the user
class user {
    username: string = "";
}

// =class includes the player setup and methods for gameplay
class gamePlay {
    player1 = new user;
    player2 = new computer;

    //creates a new player1 with the user's name
    newPlayer() {
        let player1 = new user;
        let name = player1.username;
    
        prompt("please enter username");
        console.log(prompt);
        name = JSON.stringify(prompt);
    } 
    
    // creates a new player2 with computer as user
    newComp() {
        let player2 = new computer;
        console.log(player2);
    }
}

//class for making a game choice with methods that choose one of 3 objects
class gameChoice {
    p1choice: number = 0;
    p2choice: number = 0
    
    selectRock(rock = 1) {
        if (onclick) {
            this.p1choice = rock
            console.log(this.p1choice)        
        }
    }

    selectPaper(paper = 2) {
        if (onclick) {
            this.p1choice = paper
            console.log(this.p1choice)
        }
    }

    selectScissors(scissors = 3) {
        if (onclick) {
            this.p1choice = scissors
            console.log(this.p1choice)
        }
    }

    computerSelects(pieces = [1, 2, 3] ) {
        const rand = Math.floor(Math.random() * pieces.length);
        this.p2choice = pieces[rand]
        console.log(this.p2choice);
    }

}

function winnerIs() {
    let p1Wins = new gameChoice
    let p2WIns = new gameChoice
    let p1 = new gamePlay
    let p2 = new gamePlay
    if (p1Wins.p1choice < p2WIns.p2choice) {
        alert("$(p1.player1.username) Loses!")
    } else if (p1Wins.p1choice > p2WIns.p2choice) {
        alert("$(p1.player1.username) Wins!")
    } else if (p1Wins.p1choice = p1Wins.p2choice) {
        alert("$(p1.player1.username) and Computer are tied!")
    }
    
}