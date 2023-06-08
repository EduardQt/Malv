import {useEffect} from "react";
import {ServiceContainer} from "../../services/ServiceContainer";
import {useNavigate} from "react-router-dom";
import {Routes} from "../../Routes";

export function SignOutComponent() {

    const navigate = useNavigate();

    useEffect(() => {
        const authService = ServiceContainer.authService;
        const userService = ServiceContainer.userService;
        authService.signOut()
            .then(() => {
                userService.signOut();
                navigate(Routes.Index);
            })
            .catch(error => {
                console.log('here');
            });
    }, []);

    return (
        <div>
            Sign out!
        </div>
    )
}
