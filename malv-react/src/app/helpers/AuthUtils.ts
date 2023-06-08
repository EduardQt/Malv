import {ServiceContainer} from "../../services/ServiceContainer";
import {Dispatch} from "react";

export const validateAuth = (setLoading: Dispatch<boolean>) => {
    const refreshToken = ServiceContainer.userService.refreshToken;
    if (ServiceContainer.userService.authToken) {
        setLoading(false);
    } else if (refreshToken) {
        ServiceContainer.authService.authFromRefreshToken({
            refreshToken: refreshToken
        })
            .then(result => {
                ServiceContainer.userService.authToken = result;
                ServiceContainer.userService.refreshToken = result.refreshToken;
                setLoading(false);
            })
            .catch(error => {
                ServiceContainer.userService.signOut();
                setLoading(false);
            });
    } else {
        setLoading(false);
    }
}
