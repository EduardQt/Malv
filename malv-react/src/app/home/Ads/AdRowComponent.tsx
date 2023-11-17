import {Ad_Mod} from "../../../network/Models/Ad_Mod";
import {AdImage} from "../../../network/APIData";
import {Routes} from "../../../Routes";
import {Col, Row} from "reactstrap";
import {useNavigate} from "react-router-dom";
import {Category_Mod} from "../../../network/Models/Category_Mod";
import {CategoryType_Mod} from "../../../network/Models/CategoryType_Mod";

interface AdRowComponentProps {
    ad: Ad_Mod;
    rootCategory: Category_Mod | undefined;
}

export const AdRowComponent = ({ad, rootCategory}: AdRowComponentProps) => {
    const navigate = useNavigate();
    const adImage = AdImage(ad.id, ad.images[0]);

    const renderCar = () => {
        console.log(ad.price)
        return (
            <div>
                <h3>{ad.title}</h3>
                <span>{ad.description}</span>
                <span>{ad.price}</span>
            </div>
        )
    }

    const renderDefault = () => {
        return (
            <div>
                <h3>{ad.title}</h3>
                <span>{ad.description}</span>
            </div>
        )
    }

    const renderAdData = () => {
        if (!rootCategory) return renderDefault();

        switch (rootCategory.type) {
            case CategoryType_Mod.CARS:
                return renderCar();

            default:
                return renderDefault();
        }
    }

    return (
        <div role={'button'} onClick={() => navigate(Routes.AdViewUrl + '?id=' + ad.id)}>
            <Row className={'pb-2 pt-2'}>
                <Col sm={6} md={3} lg={3}>
                    <img src={adImage} alt={'ad'} className={'w-100'}/>
                </Col>
                <Col sm={6} md={3} lg={3}>
                    {renderAdData()}
                </Col>
            </Row>
        </div>
    )
}
