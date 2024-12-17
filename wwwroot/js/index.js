
const threshold = 10;
let seconds = 0;
let clicks = 0;
const currentScoreElement = document.getElementById("current_score");
const recordScoreElement = document.getElementById("record_score");
const profitPerClickElement = document.getElementById("profit_per_click");
const profitPerSecondElement = document.getElementById("profit_per_second");
const profitPerSecond = Number(profitPerSecondElement.innerText);
const currentScore = Number(currentScoreElement.innerText);
const recordScore = Number(recordScoreElement.innerText);
const profitPerClick = Number(profitPerClickElement.innerText);


$(document).ready(function() {
    const clickitem = document.getElementById("clickitem");

    clickitem.onclick = click;
    setInterval(addSecond, 1000);

    const boostButtons = document.getElementsByClassName("boost-button");

    for (let i = 0; i < boostButtons.length; i++) {
        const boostButton = boostButtons[i];

        boostButton.onclick = () => boostButtonClick(boostButton);
    }
})

function boostButtonClick(boostButton) {
    const boostIdElement = boostButton.getElementsByClassName("boost-id")[0];
    const boostId = boostIdElement.innerText;

    buyBoost(boostId);
}

function buyBoost(boostId) {
    $.ajax({
        url: '/boost/buy',
        method: 'post',
        dataType: 'json',
        data: { boostId: boostId },
        success: onAddPointsSuccess(),
    });
}

function addSecond() {
    seconds++;

    if (seconds >= threshold) {
        addPointsToScore();
    }

    if (seconds > 0) {
        updateUiScore();
    }
}


function click() {
    clicks++;

    if (clicks >= threshold) {
        addPointsToScore();
    }

    if (clicks > 0) {
        updateUiScore();
    }
}

function updateUiScore() {
    var profit = clicks * profitPerClick + seconds * profitPerSecond;
    const uiCurrentScore = currentScore + profit;
    const uiRecordScore = recordScore + profit;

    currentScoreElement.innerText = uiCurrentScore;
    recordScoreElement.innerText = uiRecordScore;
}


function addPointsToScore() {
    $.ajax({
        url: '/score',
        method: 'post',
        dataType: 'json',
        data: { clicks: clicks, seconds: seconds },
        success: onAddPointsSuccess(),
    });
}

function onAddPointsSuccess() {
    seconds = 0;
    clicks = 0;
    location.reload();
}