import {useNavigate, useSearchParams} from "react-router-dom";
import {useEffect, useState} from "react";
import {Ad_Mod} from "../../../network/Models/Ad_Mod";
import {ServiceContainer} from "../../../services/ServiceContainer";
import {Button, Col, Container, Modal, Row, Spinner} from "reactstrap";
import {AdCarouselView} from "../../shared/AdCarouselView";
import {Routes} from "../../../Routes";
import {CarButtonDescComponent} from "./Misc/CarButtonDescComponent";
import {
    faCalendar,
    faCar,
    faCarSide, faClose,
    faGasPump,
    faGears,
    faTachometer,
    faTachometerFast
} from "@fortawesome/free-solid-svg-icons";
import {getCarDriveType, getCarType, getFuelType, getTransmissionType} from "./Misc/CarDataHelpers";
import './AdViewComponent.css'
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {AdGalleryComponent} from "./Misc/AdGalleryComponent";

export const AdViewComponent = () => {
    const [searchParams] = useSearchParams();
    const [loading, setLoading] = useState<boolean>(true);
    const [ad, setAd] = useState<Ad_Mod | null>(null);
    const [modalOpen, setModalOpen] = useState<boolean>(false);
    const [activeIndex, setActiveIndex] = useState<number>(0);

    useEffect(() => {
        if (!searchParams.has('id')) {
            console.log('no id!');
        } else {
            const id = searchParams.get('id');
            ServiceContainer.adService
                .get(parseInt(id as string))
                .then(result => {
                    setAd(result);
                    setLoading(false);
                })
                .catch(error => {
                    console.log(error);
                    setLoading(false);
                });
        }
    }, [searchParams]);

    const renderLoading = () => {
        return (
            <div>
                <Spinner size={"sm"}/>
            </div>
        )
    }

    const renderError = () => {
        return (
            <div>
                error
            </div>
        )
    }

    const addWatcher = () => {
        if (!ad) return;

        const adService = ServiceContainer.adService;
        adService.addAdWatch({
            adId: ad.id
        }).then(() => {

        }).catch(error => {

        })
    }

    const navigate = useNavigate();

    if (loading) {
        return renderLoading();
    } else if (!ad) {
        return renderError();
    } else {
        return (
            <Container>
                <Modal isOpen={modalOpen}>
                    <div style={{
                        zIndex: 2,
                        color: 'white',
                        position: 'absolute',
                        right: 8,
                        top: 5,
                        cursor: "pointer",
                        fontSize: 24
                    }} onClick={() => setModalOpen(false)}>
                        <FontAwesomeIcon icon={faClose} />
                    </div>
                    <AdCarouselView adId={ad.id} images={ad.images} activeIndex={activeIndex}/>
                </Modal>
                <Row>
                    <h3>{ad.title}</h3>
                </Row>
                <Row className={'vh-40'}>
                    <Col sm={12} md={4} lg={4}>
                        <h2><span className={'color-primary'}>SEK </span>{ad.price}</h2>
                        <h4><span className={'color-primary mt-3'}>Sold by: </span>{ad.author}</h4>
                        <Button color={'primary'} className={'w-100 mt-3'} onClick={() => navigate(Routes.MyChatsUrl)}>Contact</Button>
                        <Button color={'outline-primary'} className={'w-100 mt-3'} onClick={addWatcher}>Add watcher</Button>
                    </Col>
                    <Col className={'h-100 d-flex bg-dark'} sm={12} md={8} lg={8}>
                        <AdGalleryComponent adId={ad.id} images={ad.images} onClick={(index) => {
                            setModalOpen(true);
                            setActiveIndex(index);
                        }} index={activeIndex} setIndex={(index) => {
                            setActiveIndex(index);
                            setModalOpen(true);
                        }}/>
                    </Col>
                </Row>

                {ad.carAd && (
                    <div className={'car-ad-data text-center'}>
                        <CarButtonDescComponent icon={faGasPump} title={'Fuel type'} desc={getFuelType(ad.carAd)} />
                        <CarButtonDescComponent icon={faGears} title={'Gearbox'} desc={getTransmissionType(ad.carAd)} />
                        <CarButtonDescComponent icon={faTachometer} title={'Mileage'} desc={ad.carAd.mileage.toString()} />
                        <CarButtonDescComponent icon={faCalendar} title={'Model year'} desc={ad.carAd.age.toString()} />
                        <CarButtonDescComponent icon={faCar} title={'Car type'} desc={getCarType(ad.carAd)} />
                        <CarButtonDescComponent icon={faCarSide} title={'Driving'} desc={getCarDriveType(ad.carAd)} />
                        <CarButtonDescComponent icon={faTachometerFast} title={'Horsepower'} desc={ad.carAd.horsePower.toString()} />
                    </div>
                )}

                <h1 className={'color-primary'}>Description</h1>
                <p className={'lead'}>{ad.description}</p>
            </Container>
        )
    }
}
