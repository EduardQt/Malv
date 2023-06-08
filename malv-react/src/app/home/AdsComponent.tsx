import {useEffect, useState} from "react";
import {Button, Col, Container, Input, Row, Spinner} from "reactstrap";
import {useLocation, useNavigate} from "react-router-dom";
import {ServiceContainer} from "../../services/ServiceContainer";
import {Ad_Mod} from "../../network/Models/Ad_Mod";
import {AdImage} from "../../network/APIData";
import {MunicipalityPicker, MunicipalitySelection} from "../shared/MunicipalityPicker";
import {Routes} from "../../Routes";
import {Category_Mod} from "../../network/Models/Category_Mod";
import {AdsCategoriesComponent} from "./Ads/AdsCategoriesComponent";
import {CarCategoryPicker} from "../shared/CarCategoryPicker";
import {CategoryType_Mod} from "../../network/Models/CategoryType_Mod";

export interface AdsComponentRouteState {
    query: string;
    municipalityId: number | null;
    categoryId: number | null;
}

export const AdsComponent = () => {

    const [ads, setAds] = useState<Ad_Mod[]>([]);
    const [loading, setLoading] = useState<boolean>(true);
    const [citySelection, setCitySelection] = useState<MunicipalitySelection>({
        municipalityId: null
    });
    const [categoryId, setCategoryId] = useState<number | null>(null);
    const [query, setQuery] = useState<string>('');
    const location = useLocation();
    const navigate = useNavigate();

    const [categories, setCategories] = useState<Category_Mod[]>([]);
    const [rootCategory, setRootCategory] = useState<Category_Mod>();

    useEffect(() => {
        const {routeState} = location.state;
        const adService = ServiceContainer.adService;
        if (routeState) {
            const mapped = routeState as AdsComponentRouteState;
            setCitySelection({
                municipalityId: mapped.municipalityId
            });
            setQuery(mapped.query);
            adService.search({
                query: mapped.query,
                municipalityId: mapped.municipalityId,
                categoryId: mapped.categoryId
            }).then(result => {
                setAds(result.ads);
                setCategories(result.categories);
                setRootCategory(result.rootCategory);
                setLoading(false);
            }).catch(error => {
                setLoading(false);
            });
        } else {
            setLoading(false);
        }
    }, []);

    const search = (categoryId: number | null) => {
        const adService = ServiceContainer.adService;
        setLoading(true);
        adService.search({
            query: query,
            municipalityId: citySelection.municipalityId,
            categoryId: categoryId
        }).then(result => {
            setAds(result.ads);
            setCategories(result.categories);
            setLoading(false);
        }).catch(error => {
            setLoading(false);
        });
    }

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
            <div>
                <Spinner size={"sm"}/>
            </div>
        )
    }

    return (
        <Container>
            <Row>
                <Col sm={12} md={6} lg={6}>
                    <Input placeholder={'Search'} value={query} onChange={(event) => setQuery(event.target.value)}/>
                </Col>
                <Col sm={12} md={3} lg={3}>
                    <MunicipalityPicker selection={citySelection} setSelection={setCitySelection} hideLabel={true}/>
                </Col>
                <Col sm={12} md={3} lg={3}>
                    <Button className={'w-100'} color={'primary'} onClick={() => search(categoryId)}>Search</Button>
                </Col>
            </Row>
            {rootCategory && rootCategory.type === CategoryType_Mod.CARS ?
                (
                    <CarCategoryPicker rootCategory={rootCategory} categories={categories} setCategory={categoryId => {
                        setCategoryId(categoryId);
                        search(categoryId);
                    }}/>
                )
                :
                (
                    <AdsCategoriesComponent categories={categories} setCategory={categoryId => {
                        setCategoryId(categoryId);
                        search(categoryId);
                    }}/>
                )}
            {loading ? renderLoader() : renderAds()}
        </Container>
    )
}
