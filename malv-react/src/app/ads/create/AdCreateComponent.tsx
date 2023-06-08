import {useEffect, useState} from "react";
import {ServiceContainer} from "../../../services/ServiceContainer";
import {Category_Mod} from "../../../network/Models/Category_Mod";
import {Button, Form, FormGroup, Input, Label, Row, Spinner} from "reactstrap";
import {ImageUploaderView} from "../../shared/ImageUploaderView";
import {CarAd_Mod} from "../../../network/Models/CarAd_Mod";
import {AdCreateCarComponent} from "./AdCreateCarComponent";
import {AsyncFile} from "../../helpers/FileUtils";
import {Municipality_Mod} from "../../../network/Models/Municipality_Mod";
import {CategoryPicker} from "../../shared/CategoryPicker";
import {CategoryType_Mod} from "../../../network/Models/CategoryType_Mod";

export function AdCreateComponent() {

    const [municipalities, setMunicipalities] = useState<Municipality_Mod[]>([]);
    const [categories, setCategories] = useState<Category_Mod[]>([]);
    const [loading, setLoading] = useState<boolean>(true);

    const [title, setTitle] = useState<string>('');
    const [desc, setDesc] = useState<string>('');
    const [category, setCategory] = useState<Category_Mod>(categories[0]);
    const [carAdMod, setCarAdMod] = useState<CarAd_Mod | null>(null);
    const [images, setImages] = useState<AsyncFile[]>([]);

    const [municipality, setMunicipality] = useState<Municipality_Mod>();

    useEffect(() => {
        ServiceContainer.adService.getCreate()
            .then(result => {
                setCategories(result.categories);
                setCategory(result.categories[0]);

                const resultMunicipalities = result.country.municipalities;
                setMunicipalities(resultMunicipalities);
                setMunicipality(resultMunicipalities[0]);

                setLoading(false);
            });
    }, []);

    const renderLoader = () => {
        return (
            <div className={'d-flex flex-grow-1 justify-content-center align-items-center'}>
                <Spinner size={"lg"}/>
            </div>
        )
    }

    const onSubmit = (event: any) => {
        if (!municipality) return;

        ServiceContainer.adService.adCreate({
            files: images.map(value => value.file),
            title: title,
            description: desc,
            carAdMod: carAdMod ? JSON.stringify(carAdMod) : null,
            categoryId: category.id,
            municipalityId: municipality.id
        }).then(result => {
            console.log(result);
        });
    }

    const onMunicipalityChange = (event: any) => {
        const municipality = municipalities.find(f => f.id === parseInt(event.target.value, 10));
        if (!municipality) {
            return;
        }

        setMunicipality(municipality);
    }

    const renderContent = () => {
        return (
            <div className="ms-auto me-auto col-lg-6 col-md-8 col-sm-12">
                <Form encType={'multipart/form-data'}>
                    <FormGroup>
                        <Label for="adCreateTitle">
                            Title
                        </Label>
                        <Input id={'adCreateTitle'} type={'text'} value={title}
                               onChange={(event) => setTitle(event.target.value)}/>
                    </FormGroup>
                    <FormGroup>
                        <Label for="adCreateDesc">
                            Description
                        </Label>
                        <Input id={'adCreateDesc'} type={'textarea'} value={desc}
                               onChange={(event) => setDesc(event.target.value)}/>
                    </FormGroup>
                    <Row>
                        <div className={'col col-sm-12 col-md-8 col-lg-6'}>
                            <Label for="adCreateMunicipality">
                                Municipality
                            </Label>
                            <Input id={'adCreateMunicipality'} type={'select'}
                                   onChange={onMunicipalityChange}>
                                {municipalities.map((value, index) => {
                                    return (
                                        <option value={value.id} key={value.id}>{value.name}</option>
                                    )
                                })}
                            </Input>
                        </div>
                    </Row>
                    <CategoryPicker categories={categories} setCategory={category => setCategory(category)} />
                    {(category && category.type === CategoryType_Mod.CARS) && (<AdCreateCarComponent carAd={carAdMod} setCarAd={setCarAdMod} />)}
                    <ImageUploaderView images={images} setImages={setImages}/>
                    <FormGroup>
                        <Button onClick={onSubmit}>
                            Create
                        </Button>
                    </FormGroup>
                </Form>
            </div>
        )
    }

    const renderCategoryOptions = (categories: Category_Mod[]) => {
        return categories.map((value, index) => {
            return value.children.length === 0 ? (
                <option value={value.id}>{value.title}</option>
            ) : renderCategoryOptGroup(value);
        })
    }

    const renderCategoryOptGroup = (optGroup: Category_Mod) => {
        return (
            <optgroup label={optGroup.title}>
                <option value={optGroup.id}>Shit</option>
                {renderCategoryOptions(optGroup.children)}
            </optgroup>
        )
    }

    return loading ? renderLoader() : renderContent();
}
