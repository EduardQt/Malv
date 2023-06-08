import {useEffect, useState} from "react";
import {Ad_Mod} from "../../../network/Models/Ad_Mod";
import {ServiceContainer} from "../../../services/ServiceContainer";
import {Col, Container, Row, Spinner} from "reactstrap";
import {AdImage} from "../../../network/APIData";
import {Routes} from "../../../Routes";
import {useNavigate} from "react-router-dom";

export const AdWatchesComponent = () => {

    const [loading, setLoading] = useState<boolean>(true);
    const [ads, setAds] = useState<Ad_Mod[]>([]);
    const navigate = useNavigate();

    useEffect(() => {
        const adService = ServiceContainer.adService;

        setLoading(true);
        adService.getWatches()
            .then(result => {
                setAds(result.ads);
                setLoading(false);
            })
            .catch(error => {
                console.log(error);
                setLoading(false);
            });

    }, []);

    const renderAds = () => {
        return ads.map((value, index) => {
            const adImage = AdImage(value.id, value.images[0]);
            return (
                <div key={index} role={'button'} onClick={() => navigate(Routes.AdViewUrl + '?id=' + value.id)}>
                    <Row className={'pb-2 pt-2'}>
                        <Col sm={6} md={3} lg={3}>
                            <img src={adImage} alt={'ad'} className={'w-100'}/>
                        </Col>
                        <Col sm={6} md={3} lg={3}>
                            <h6>{value.title}</h6>
                            <span>{value.description}</span>
                        </Col>
                    </Row>
                </div>
            )
        });
    }

    const renderLoader = () => {
        return (
            <div className={'d-flex flex-grow-1 justify-content-center align-items-center'}>
                <Spinner size={"lg"}/>
            </div>
        )
    }

    const renderContent = () => {
        return (
            <Container>
                <Row>
                    {renderAds()}
                </Row>
            </Container>
        )
    }

    return loading ? renderLoader() : renderContent();
}
