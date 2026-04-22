from src.prompt import AcceptList
from pytest import approx
import pytest

def test_accept_list():
    assert AcceptList([3, 7, 12, 5, 9]) == approx(7.2,abs=0.01)