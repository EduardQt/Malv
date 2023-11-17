import {useEffect, useState} from "react";
import {Container, Spinner} from "reactstrap";
import {ServiceContainer} from "../../services/ServiceContainer";
import {LandingSearchComponent} from "./Landing/LandingSearchComponent";
import {Category_Mod} from "../../network/Models/Category_Mod";
import {CategoryType_Mod} from "../../network/Models/CategoryType_Mod";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {
    faCar,
    faCouch,
    faDesktop,
    faHouse, faIndustry,
    faShirt,
    faTableTennisPaddleBall,
    faUserDoctor
} from '@fortawesome/free-solid-svg-icons'
import {IconDefinition} from "@fortawesome/fontawesome-common-types";
import './Styles/LandingPageComponent.css'
import {Routes} from "../../Routes";
import {useNavigate} from "react-router-dom";
import {AdsComponentRouteState} from "./AdsComponent";
import {AssetImage} from "../../network/APIData";

export const LandingPageComponent = () => {

    const [loading, setLoading] = useState<boolean>(true);
    const [baseCategories, setBaseCategories] = useState<Category_Mod[]>([]);

    const navigate = useNavigate();

    const search = (categoryId: number) => {
        const navigationState: AdsComponentRouteState = {
            municipalityId: null,
            query: '',
            categoryId: categoryId
        };
        navigate(Routes.AdsUrl, {
            state: {
                routeState: navigationState
            }
        });
    }

    useEffect(() => {
        const adService = ServiceContainer.adService;

        setLoading(true);
        adService.baseCategories()
            .then(baseCategories => {
                setBaseCategories(baseCategories);
                setLoading(false);
            })
            .catch(error => {
                setLoading(false);
            });
    }, []);

    const renderBaseCategories = () => {
        return baseCategories.map((value, index) => {
            let faIcon: IconDefinition | null = null;

            switch (value.type) {
                case CategoryType_Mod.CARS:
                    faIcon = faCar;
                    break;
                case CategoryType_Mod.JOBS:
                    faIcon = faUserDoctor;
                    break;
                case CategoryType_Mod.FOR_HOME:
                    faIcon = faCouch;
                    break;
                case CategoryType_Mod.HOUSING:
                    faIcon = faHouse;
                    break;
                case CategoryType_Mod.PERSONAL:
                    faIcon = faShirt;
                    break;
                case CategoryType_Mod.ELECTRONICS:
                    faIcon = faDesktop;
                    break;
                case CategoryType_Mod.HOBBIES:
                    faIcon = faTableTennisPaddleBall;
                    break;
                case CategoryType_Mod.WORK_OPERATIONS:
                    faIcon = faIndustry;
                    break;
            }

            return (
                <div role={'button'} className={'m-2 text-center'} onClick={() => search(value.id)}>
                    <div className={'base-category-icon'}>
                        {faIcon && (
                            <FontAwesomeIcon icon={faIcon} fontSize={20}/>
                        )}
                    </div>
                    <h5>{value.title}</h5>
                </div>
            )
        });
    }

    const renderContent = () => {
        return (
            <Container>
                <div style={{
                    background: `url(${AssetImage('landing_search_bg.jpg')}) no-repeat center center fixed`,
                    backgroundSize: 'cover'
                }}>
                    <LandingSearchComponent/>
                </div>
                <div className={'d-flex text-center justify-content-center'}>
                    {renderBaseCategories()}
                </div>
            </Container>
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
