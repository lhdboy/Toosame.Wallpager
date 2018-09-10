import React, { Component } from 'react';
import { ListGroup, ListGroupItem } from 'react-bootstrap';

export class TagButton extends Component {
    displayName = TagButton.name

    render() {
        return (
            <ListGroup>
                <ListGroupItem header="Heading 1">Some body text</ListGroupItem>
                <ListGroupItem header="Heading 2" href="#">
                    Linked item
                </ListGroupItem>
                <ListGroupItem header="Heading 3" bsStyle="danger">
                    Danger styling
                </ListGroupItem>
            </ListGroup>
        );
    }
}
