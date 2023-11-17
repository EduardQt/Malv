import {useEffect, useState} from "react";
import {Button, Col, Container, Input, InputGroup, Row, Spinner} from "reactstrap";
import {useLocation} from "react-router-dom";
import {ServiceContainer} from "../../services/ServiceContainer";
import {Ad_Mod} from "../../network/Models/Ad_Mod";
import {MunicipalityPicker, MunicipalitySelection} from "../shared/MunicipalityPicker";
import {Category_Mod} from "../../network/Models/Category_Mod";
import {AdsCategoriesComponent} from "./Ads/AdsCategoriesComponent";
import {AdRowComponent} from "./Ads/AdRowComponent";
import {Search_Order_Mod} from "../../network/Models/Search_Order_Mod";

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
                categoryId: mapped.categoryId,
                order: Search_Order_Mod.DATE_ASC,
                carFilter: {}
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
            categoryId: categoryId,
            order: Search_Order_Mod.DATE_ASC,
            carFilter: {}
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
            return (
                <AdRowComponent ad={value} key={index} rootCategory={rootCategory} />
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
                <Col sm={12} md={9} lg={9}>
                    <InputGroup>
                        <Input placeholder={'Search'} value={query} onChange={(event) => setQuery(event.target.value)}/>
                        <Button color={'primary'} onClick={() => search(categoryId)}>Search</Button>
                    </InputGroup>
                </Col>
                <Col sm={12} md={3} lg={3}>
                    <MunicipalityPicker selection={citySelection} setSelection={setCitySelection} hideLabel={true}/>
                </Col>
            </Row>
            <div className={'horizontal-divider mt-2 mb-2'} />
            <Row>
                <Col sm={12} md={3} lg={3}>
                    <AdsCategoriesComponent categories={categories} setCategory={categoryId => {
                        setCategoryId(categoryId);
                        search(categoryId);
                    }}/>
                </Col>
                <Col sm={12} md={9} lg={9}>
                    {loading ? renderLoader() : renderAds()}
                </Col>
            </Row>
        </Container>
    )
}
