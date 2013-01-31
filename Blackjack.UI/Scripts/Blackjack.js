$(document).ready(function () {
    $.getJSON("/Home/PlayerHasBlackjack", { playerIndex: 0 }, function (hasBlackjack) {
        if (hasBlackjack) {
            $("h1#message").text("Player has Blackjack!").show(200);
        }
        else {
            $("button#hit").show();
            $("button#stand").show();
            $("button#doubledown").show();
        }
    });

    $("button#hit").click(function () {
        $.getJSON("/Home/GiveCardToPlayer", { playerIndex: 0 }, function (dealtCard) {
            $("<li>" + dealtCard.Face + " of " + dealtCard.Suit + "</li>").hide().appendTo("ul#player1").show(200);
            $.getJSON("/Home/PlayerLost", { playerIndex: 0 }, function (playerLost) {
                if (playerLost) {
                    $.getJSON("/Home/GiveCardToDealer", null, function (dealtCard) {
                        $("li#unknown").hide().text(dealtCard.Face + " of " + dealtCard.Suit).delay(200).show(200);
                        $("h1#message").text("You lose!").delay(400).show(200);
                    });

                    $("button#hit").hide();
                    $("button#stand").hide();
                }
                $("button#doubledown").hide();
                $("button#split").hide();
            });
        });
    });

    $("button#stand").click(function () {
        $.getJSON("/Home/GiveCardToDealer", null, function (dealtCard) {
            $("li#unknown").hide().text(dealtCard.Face + " of " + dealtCard.Suit).show(200);

            $.getJSON("/Home/DealerHasBlackjack", null, function (hasBlackjack) {
                if (hasBlackjack) {
                    $("h1#message").text("Dealer has Blackjack!").delay(200).show(200);
                }
                else {
                    $.getJSON("/Home/GiveAllCardsToDealer", null, function (dealtCards) {
                        var i;
                        for (i = 0; i < dealtCards.length; i++) {
                            $("<li>" + dealtCards[i].Face + " of " + dealtCards[i].Suit + "</li>").hide().appendTo("ul#dealer").delay((i + 1) * 200).show(200);
                        }

                        $.getJSON("/Home/PlayerWins", { playerIndex: 0 }, function (playerWins) {
                            switch (playerWins) {
                                case -1:
                                    $("h1#message").text("You lose!").delay(i * 200).show(200);
                                    break;
                                case 0:
                                    $("h1#message").text("Draw!").delay(i * 200).show(200);
                                    break;
                                case 1:
                                    $("h1#message").text("You win!").delay(i * 200).show(200);
                                    break;
                            }
                        });
                    });
                }
            });

            $("button#hit").hide();
            $("button#stand").hide();
            $("button#doubledown").hide();
            $("button#split").hide();
        });
    });

    $("button#doubledown").click(function () {
        $.getJSON("/Home/GiveCardToPlayer", { playerIndex: 0 }, function (dealtCard) {
            $("<li>" + dealtCard.Face + " of " + dealtCard.Suit + "</li>").hide().appendTo("ul#player1").show(200);
            $.getJSON("/Home/PlayerLost", { playerIndex: 0 }, function (playerLost) {
                $.getJSON("/Home/GiveCardToDealer", null, function (dealtCard) {
                    $("li#unknown").hide().text(dealtCard.Face + " of " + dealtCard.Suit).delay(200).show(200);
                });

                if (playerLost) {
                    $("h1#message").text("You lose!").delay(400).show(200);
                }
                else {
                    $.getJSON("/Home/GiveAllCardsToDealer", null, function (dealtCards) {
                        var i;
                        for (i = 0; i < dealtCards.length; i++) {
                            $("<li>" + dealtCards[i].Face + " of " + dealtCards[i].Suit + "</li>").hide().appendTo("ul#dealer").delay((i + 1) * 200).show(200);
                        }

                        $.getJSON("/Home/PlayerWins", { playerIndex: 0 }, function (playerWins) {
                            switch (playerWins) {
                                case -1:
                                    $("h1#message").text("You lose!").delay(i * 200).show(200);
                                    break;
                                case 0:
                                    $("h1#message").text("Draw!").delay(i * 200).show(200);
                                    break;
                                case 1:
                                    $("h1#message").text("You win!").delay(i * 200).show(200);
                                    break;
                            }
                        });
                    });
                }

                $("button#hit").hide();
                $("button#stand").hide();
                $("button#doubledown").hide();
                $("button#split").hide();
            });
        });
    });
});