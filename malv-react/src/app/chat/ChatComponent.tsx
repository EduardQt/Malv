import {Outlet} from "react-router-dom";
import {Navigator} from "../shared/Navigator";
import {useEffect, useState} from "react";
import {validateAuth} from "../helpers/AuthUtils";
import {Spinner} from "reactstrap";

export function ChatComponent() {
    const [loading, setLoading] = useState<boolean>(true);

    useEffect(() => {
        setLoading(true);
        validateAuth(setLoading);
    }, []);

    const renderContent = () => {
        return (
            <div className={'d-flex flex-column vh-100'}>
                <Navigator/>
                <Outlet />
            </div>
        )
    }

    const renderLoader = () => {
        return (
            <div>
                <Spinner size={"sm"}/>
            </div>
        )
    }

    return loading ? renderLoader() : renderContent();
}
