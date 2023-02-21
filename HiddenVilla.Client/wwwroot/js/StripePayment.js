redirectToCheckout = function (sessionId) {
    var stripe = Stripe('pk_test_51MULCuAAr3TSRZRA6DNRu9ktjT2nNOGT3ucGnGoxrlq9EaR7bSIgqQgv0OVNza6oxQEoFYEylcoTmUbyC6PYFsp700XiHlUmRd');
    stripe.redirectToCheckout({
        sessionId: sessionId
    });
};