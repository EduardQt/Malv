import {
    Badge,
    Collapse, DropdownItem, DropdownMenu, DropdownToggle, Navbar,
    NavbarBrand, NavbarToggler,
    NavItem,
    NavLink,
    UncontrolledDropdown
} from "reactstrap";
import {useEffect, useState} from "react";
import {Link} from "react-router-dom";
import {Routes} from "../../Routes";
import {ServiceContainer} from "../../services/ServiceContainer";

export function Navigator() {
    const [isOpen, setIsOpen] = useState(false);
    const [unreadChats, setUnreadChats] = useState<number>(0);

    const toggle = () => setIsOpen(!isOpen);

    useEffect(() => {
        const chatService = ServiceContainer.chatService;

        if (ServiceContainer.userService.authToken) {
            chatService.unreadMessages()
                .then(result => {
                    setUnreadChats(result.count);
                })
                .catch(error => {
                    console.log('error!');
                });
        }
    }, []);

    const renderMyChatsNavLink = () => {
        if (unreadChats > 0) {
            return (<NavLink tag={Link} className="text-dark" to={Routes.MyChatsUrl}>Messages <Badge color="primary">{unreadChats}</Badge></NavLink>);
        }

        return (<NavLink tag={Link} className="text-dark" to={Routes.MyChatsUrl}>Messages</NavLink>);
    }

    const renderUsername = () => {
        if (unreadChats > 0) {
            return (<span>{ServiceContainer.userService.authTokenAbsolute.username} <Badge color="primary">{unreadChats}</Badge></span>);
        }

        return (<span>{ServiceContainer.userService.authTokenAbsolute.username}</span>);
    }

    return (
        <header>
            <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" container
                    light>
                <NavbarBrand tag={Link} to="/">Malv.React</NavbarBrand>
                <NavbarToggler onClick={toggle} className="mr-2"/>
                <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={isOpen} navbar>
                    {ServiceContainer.userService.authToken ? (
                        <ul className="navbar-nav flex-grow">
                            <UncontrolledDropdown nav inNavbar>
                                <DropdownToggle nav caret>
                                    {renderUsername()}
                                </DropdownToggle>
                                <DropdownMenu end>
                                    <DropdownItem>
                                        <NavItem>
                                            {renderMyChatsNavLink()}
                                        </NavItem>
                                    </DropdownItem>
                                    <DropdownItem>
                                        <NavItem>
                                            <NavLink tag={Link} className="text-dark" to={Routes.AdCreateUrl}>Create ad</NavLink>
                                        </NavItem>
                                    </DropdownItem>
                                    <DropdownItem>
                                        <NavItem>
                                            <NavLink tag={Link} className="text-dark" to={Routes.AdWatchesUrl}>Ad watches</NavLink>
                                        </NavItem>
                                    </DropdownItem>
                                    <DropdownItem divider />
                                    <DropdownItem>
                                        <NavItem>
                                            <NavLink tag={Link} className="text-dark" to={Routes.SignOutUrl}>Sign out</NavLink>
                                        </NavItem>
                                    </DropdownItem>
                                </DropdownMenu>
                            </UncontrolledDropdown>
                        </ul>
                    ) : (
                        <ul className="navbar-nav flex-grow">
                            <NavItem>
                                <NavLink tag={Link} className="text-dark" to={Routes.LoginUrl}>Login</NavLink>
                            </NavItem>
                            <NavItem>
                                <NavLink tag={Link} className="text-dark" to={Routes.RegisterUrl}>Register</NavLink>
                            </NavItem>
                        </ul>
                    )}

                </Collapse>
            </Navbar>
        </header>
    );

}
