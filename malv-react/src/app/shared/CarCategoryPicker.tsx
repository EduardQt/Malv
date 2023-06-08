import {Category_Mod} from "../../network/Models/Category_Mod";
import {Col, Dropdown, DropdownItem, DropdownMenu, DropdownToggle, FormGroup, Input, Label, Row} from "reactstrap";
import {useEffect, useState} from "react";
import * as React from "react";
import {searchCategories} from "../helpers/CategoryHelpers";
import {debounceTime, Subject} from "rxjs";

interface CarCategoryPickerProps {
    rootCategory: Category_Mod;
    categories: Category_Mod[];
    setCategory: (category: number) => void;
}

export const CarCategoryPicker = ({rootCategory, categories, setCategory}: CarCategoryPickerProps) => {
    const [brandDropdownOpen, setBrandDropdownOpen] = useState<boolean>(false);
    const [modelDropdownOpen, setModelDropdownOpen] = useState<boolean>(false);

    const [brandCategory, setBrandCategory] = useState<Category_Mod | null>(null);
    const [modelCategory, setModelCategory] = useState<Category_Mod | null>(null);

    const [searchText, setSearchText] = useState<string>('');
    const [filtered, setFiltered] = useState<Category_Mod[]>([]);

    const subscribe$ = new Subject<string>();
    const subscription = subscribe$.pipe(debounceTime(250)).subscribe((result) => {
        filterSearches(result);
    });

    useEffect(() => {
        setFiltered(categories);
        return () => {
            subscription.unsubscribe();
            subscribe$.complete();
        }
    }, [categories]);

    const renderModels = (parents: Category_Mod[], level: number) => {
        return parents.map((child, index) => {
            return (
                <div style={{
                    paddingLeft: level * 3 + 'px'
                }}>
                    <DropdownItem key={'category-' + child.id} onClick={() => {
                        setModelCategory(child);
                        setCategory(child.id);
                    }}>
                        {child.title}
                    </DropdownItem>
                    {renderModels(child.children, level + 1)}
                </div>
            )
        });
    }

    const renderBrands = () => {
        return rootCategory.children.map((value, index) => {
            return (
                <div style={{
                    paddingLeft: 3 + 'px'
                }}>
                    <DropdownItem key={'category-' + value.id} onClick={() => {
                        setCategory(value.id);
                        setBrandCategory(value);
                        setFiltered(value.children);
                    }}>
                        {value.title}
                    </DropdownItem>
                </div>
            )
        });
    }

    const onSearchChange = (text: string) => {
        setSearchText(text);
        subscribe$.next(text);
    }

    const filterSearches = (text: string) => {
        const clone = structuredClone(categories);
        const filteredCategories = searchCategories(clone, text);
        setFiltered(filteredCategories);
    }

    const getBrandTitle = () => brandCategory?.title ?? 'Brand';

    const getModelTitle = () => modelCategory?.title ?? 'Model';

    return (
        <Row>
            <Col sm={12} md={3} lg={3}>
                <FormGroup>
                    <Label for="adSelectBrand">
                        Brand
                    </Label>
                    <Dropdown isOpen={brandDropdownOpen} className={'dropdown'}
                              toggle={() => setBrandDropdownOpen(!brandDropdownOpen)}>
                        <DropdownToggle className={'input-group'}>
                            {getBrandTitle()}
                        </DropdownToggle>
                        <DropdownMenu style={{
                            maxHeight: '300px',
                            overflowX: 'scroll',
                            width: '100%'
                        }}>
                            {renderBrands()}
                        </DropdownMenu>
                    </Dropdown>
                </FormGroup>
            </Col>
            <Col sm={12} md={3} lg={3} >
                {brandCategory && <FormGroup>
                    <Label for="adSelectModel">
                        Category
                    </Label>
                    <Dropdown isOpen={modelDropdownOpen} className={'dropdown'}
                              toggle={() => setModelDropdownOpen(!modelDropdownOpen)}>
                        <DropdownToggle className={'input-group'}>
                            {getModelTitle()}
                        </DropdownToggle>
                        <DropdownMenu style={{
                            maxHeight: '300px',
                            overflowX: 'scroll',
                            width: '100%'
                        }}>
                            <Input placeholder={'Search'} value={searchText}
                                   onChange={(event) => onSearchChange(event.target.value)}/>
                            {renderModels(filtered, 1)}
                        </DropdownMenu>
                    </Dropdown>
                </FormGroup>}
            </Col>
        </Row>
    )
}
