import {useNavigate, useSearchParams} from "react-router-dom";
import {useEffect, useState} from "react";
import {Ad_Mod} from "../../../network/Models/Ad_Mod";
import {ServiceContainer} from "../../../services/ServiceContainer";
import {Button, Container, Spinner} from "reactstrap";
import {AdCarouselView} from "../../shared/AdCarouselView";
import {Routes} from "../../../Routes";

export const AdViewComponent = () => {
    const [searchParams] = useSearchParams();
    const [loading, setLoading] = useState<boolean>(true);
    const [ad, setAd] = useState<Ad_Mod | null>(null);

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
                <div className={'col-lg-6 col-md-10 col-sm-12 ms-auto me-auto'}>
                    <div className={'h-25'}>
                        <AdCarouselView adId={ad.id} images={ad.images}/>
                        <span>{ad.author}</span>
                        {
                            ServiceContainer.userService.authToken && (
                                <div>
                                    <Button color={'primary'} onClick={() => navigate(Routes.MyChatsUrl)}>Contact</Button>
                                    <Button color={'primary'} onClick={addWatcher}>Add watcher</Button>
                                </div>
                            )
                        }
                        <h4>{ad.title}</h4>
                        <span>{ad.description}</span>
                    </div>
                </div>
            </Container>
        )
    }
}
