import {Category_Mod} from "../../network/Models/Category_Mod";
import {Dropdown, DropdownItem, DropdownMenu, DropdownToggle, FormGroup, Input, Label} from "reactstrap";
import {useEffect, useState} from "react";
import * as React from "react";
import {searchCategories} from "../helpers/CategoryHelpers";
import {debounceTime, Subject} from "rxjs";

interface CategoryPickerProps {
    categories: Category_Mod[];
    setCategory: (category: Category_Mod) => void;
}

export const CategoryPicker = ({categories, setCategory}: CategoryPickerProps) => {
    const [dropdownOpen, setDropdownOpen] = useState<boolean>(false);

    const [searchText, setSearchText] = useState<string>('');
    const [filtered, setFiltered] = useState<Category_Mod[]>(categories);

    const subscribe$ = new Subject<string>();
    const subscription = subscribe$.pipe(debounceTime(250)).subscribe((result) => {
        filterSearches(result);
    });

    useEffect(() => {
        return () => {
            subscription.unsubscribe();
            subscribe$.complete();
        }
    }, []);

    const toggle = () => {
        setDropdownOpen(!dropdownOpen);
    }

    const renderCategoryChildren = (parentChild: Category_Mod, level: number) => {
        return parentChild.children.map((value, index) => {
            return (
                <div style={{
                    paddingLeft: level * 3 + 'px'
                }}>
                    <DropdownItem key={'category-' + parentChild.id} onClick={() => setCategory(value)}>
                        {value.title}
                    </DropdownItem>
                    {renderCategoryChildren(value, level + 1)}
                </div>
            )
        });
    }

    const renderCategories = () => {
        return filtered.map((value, index) => {
            return (
                <div style={{
                    paddingLeft: 3 + 'px'
                }}>
                    <DropdownItem key={'category-' + value.id} onClick={() => setCategory(value)}>
                        {value.title}
                    </DropdownItem>
                    {renderCategoryChildren(value, 2)}
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

    return (
        <FormGroup>
            <Label for="adCreateCategory">
                Category
            </Label>
            <Dropdown isOpen={dropdownOpen} className={'dropdown'} toggle={toggle}>
                <DropdownToggle className={'input-group'}>
                    Category
                </DropdownToggle>
                <DropdownMenu style={{
                    maxHeight: '300px',
                    overflowX: 'scroll',
                    width: '100%'
                }}>
                    <Input placeholder={'Search'} value={searchText} onChange={(event) => onSearchChange(event.target.value)} />
                    {renderCategories()}
                </DropdownMenu>
            </Dropdown>
        </FormGroup>
    )
}
